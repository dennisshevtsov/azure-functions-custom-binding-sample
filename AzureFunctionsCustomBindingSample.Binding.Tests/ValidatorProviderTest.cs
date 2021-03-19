// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Tests
{
  using System;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Http.Features;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Binding.Validation;

  [TestClass]
  public sealed class ValidatorProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private ValidatorProvider _validatorProvider;

    [TestInitialize]
    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _validatorProvider = new ValidatorProvider();
    }

    [TestMethod]
    public void GetValidator_Should_Return_Validator_If_Endpoint_Is_Registered()
    {
      _validatorProvider.AddValidator<TestValidator>("/test/{testId}", "post");

      var testId = Guid.NewGuid();

      Setup(testId);

      var validator = _validatorProvider.GetValidator(_httpRequestMock.Object);

      Assert.IsNotNull(validator);
      Assert.IsInstanceOfType(validator, typeof(TestValidator));
    }

    [TestMethod]
    public void GetValidator_Should_Return_Null_If_Endpoint_Is_Not_Registered()
    {
      _validatorProvider.AddValidator<TestValidator>("/test/{testId}", "put");

      var testId = Guid.NewGuid();

      Setup(testId);

      var validator = _validatorProvider.GetValidator(_httpRequestMock.Object);

      Assert.IsNull(validator);
    }

    private void Setup(Guid testId)
    {
      var httpContextMock = new Mock<HttpContext>();
      var featureCollectionMock = new Mock<IFeatureCollection>();
      var routingFeatureMock = new Mock<IRoutingFeature>();

      var routeData = new RouteData();
      routeData.Values.Add("testId", testId);

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

      var serviceProviderMock = new Mock<IServiceProvider>();
      serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                         .Returns((Type type) =>
                         {
                           if (type == typeof(TestValidator))
                           {
                             return new TestValidator();
                           }

                           return null;
                         });

      httpContextMock.SetupGet(context => context.RequestServices)
                     .Returns(serviceProviderMock.Object);

      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(httpContextMock.Object);

      _httpRequestMock.SetupGet(request => request.Path)
                      .Returns(new PathString($"/test/{testId}"));

      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns("POST");
    }
  }
}
