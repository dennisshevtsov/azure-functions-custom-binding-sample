// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request.Tests
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Http.Features;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  [TestClass]
  public sealed class RequestValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private RequestValueProvider _requestValueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _requestValueProvider = new RequestValueProvider(typeof(TestRequestDto), _httpRequestMock.Object);
    }

    [TestCleanup]
    public void Cleanup() => _httpRequestMock.Object.Body?.Dispose();

    [TestMethod]
    public async Task GetValueAsync_Should_Populate_Body_And_Route_Values_To_Result_Object()
    {
      var requestDto = new TestRequestDto
      {
        GuidProperty = Guid.NewGuid(),
        IntProperty = 123,
        StringProperty = Guid.NewGuid().ToString(),
      };
      var testId = Guid.NewGuid();

      await SetupAsync(requestDto, testId);
      var value = await _requestValueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var actual = value as TestRequestDto;

      Assert.IsNotNull(actual);
      Assert.AreEqual(requestDto.GuidProperty, actual.GuidProperty);
      Assert.AreEqual(requestDto.IntProperty, actual.IntProperty);
      Assert.AreEqual(requestDto.StringProperty, actual.StringProperty);
      Assert.AreEqual(testId, actual.TestId);
    }

    private async Task SetupAsync(TestRequestDto requestDto, Guid testId)
    {
      var stream = new MemoryStream();
      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      await JsonSerializer.SerializeAsync(stream, requestDto, requestDto.GetType(), options, CancellationToken.None);
      stream.Seek(0, SeekOrigin.Begin);

      _httpRequestMock.SetupGet(request => request.Body)
                      .Returns(stream);

      var httpContextMock = new Mock<HttpContext>();

      httpContextMock.SetupGet(context => context.RequestAborted)
                     .Returns(CancellationToken.None);

      var featureCollectionMock = new Mock<IFeatureCollection>();
      var routingFeatureMock = new Mock<IRoutingFeature>();

      var routeData = new RouteData();
      routeData.Values.Add(JsonNamingPolicy.CamelCase.ConvertName(nameof(TestRequestDto.TestId)), testId);

      routingFeatureMock.SetupGet(feature => feature.RouteData)
                        .Returns(routeData);

      featureCollectionMock.Setup(features => features[It.IsAny<Type>()])
                           .Returns((Type key) =>
                           {
                             if (key == typeof(IRoutingFeature))
                             {
                               return routingFeatureMock.Object;
                             }

                             return null;
                           });

      httpContextMock.SetupGet(context => context.Features)
                     .Returns(featureCollectionMock.Object);

      var items = new Dictionary<object, object>();

      httpContextMock.SetupGet(context => context.Items)
                     .Returns(items);

      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(httpContextMock.Object);
    }

    public sealed class TestRequestDto
    {
      public Guid TestId { get; set; }

      public Guid GuidProperty { get; set; }

      public int IntProperty { get; set; }

      public string StringProperty { get; set; }
    }
  }
}
