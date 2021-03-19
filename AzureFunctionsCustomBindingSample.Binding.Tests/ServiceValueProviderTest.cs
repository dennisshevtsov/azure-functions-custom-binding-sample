// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Tests
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Binding.Service;

  [TestClass]
  public sealed class ServiceValueProviderTest
  {
    private Mock<IServiceProvider> _serviceProviderMock;
    private Mock<HttpRequest> _httpRequestMock;
    private ServiceValueProvider _valueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _serviceProviderMock = new Mock<IServiceProvider>();
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new ServiceValueProvider(typeof(TestService), _httpRequestMock.Object);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Get_Service_From_Request_Services()
    {
      Setup();

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      _serviceProviderMock.Verify(provider => provider.GetService(typeof(TestService)));
    }

    public void Setup()
    {
      _serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                          .Returns(new TestService());

      var httpContextMock = new Mock<HttpContext>();

      httpContextMock.SetupGet(context => context.RequestServices)
                     .Returns(_serviceProviderMock.Object);

      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(httpContextMock.Object);
    }
  }

  public sealed class TestService { }
}
