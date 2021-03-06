﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request.Tests
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Http.Features;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Extensions.Primitives;
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
      _requestValueProvider = new RequestValueProvider(
        typeof(TestRequestDto), _httpRequestMock.Object, CancellationToken.None);
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
      _httpRequestMock.SetupGet(request => request.Body)
                      .Returns(await RequestValueProviderTest.CreateBodyAsync(requestDto));
      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(RequestValueProviderTest.CreateHttpMock(testId).Object);
      _httpRequestMock.SetupGet(request => request.Query)
                      .Returns(RequestValueProviderTest.CreateQueryMock().Object);
    }

    private static async Task<Stream> CreateBodyAsync(TestRequestDto requestDto)
    {
      var stream = new MemoryStream();
      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      await JsonSerializer.SerializeAsync(stream, requestDto, requestDto.GetType(), options, CancellationToken.None);
      stream.Seek(0, SeekOrigin.Begin);

      return stream;
    }

    private static Mock<HttpContext> CreateHttpMock(Guid testId)
    {
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

      return httpContextMock;
    }

    private static Mock<IQueryCollection> CreateQueryMock()
    {
      var queryCollectionMock = new Mock<IQueryCollection>();
      queryCollectionMock.Setup(collection => collection.GetEnumerator())
                         .Returns(Enumerable.Empty<KeyValuePair<string, StringValues>>().GetEnumerator());

      return queryCollectionMock;
    }
  }
}
