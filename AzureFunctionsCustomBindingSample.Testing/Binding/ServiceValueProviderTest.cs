﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Testing.Binding
{
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Api.Binding.Service;

  [TestClass]
  public sealed class ServiceValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private ServiceValueProvider _valueProvider;

    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new ServiceValueProvider(typeof(TestDocument), _httpRequestMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }
  }
}
