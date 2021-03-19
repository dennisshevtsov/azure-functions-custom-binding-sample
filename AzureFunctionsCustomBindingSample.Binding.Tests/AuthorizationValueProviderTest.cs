// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Tests
{
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Binding.Authorization;

  [TestClass]
  public sealed class AuthorizationValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private Mock<IAuthorizedUserProvider> _authorizedUserProviderMock;
    private AuthorizationValueProvider _valueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new AuthorizationValueProvider(
        _httpRequestMock.Object,
        _authorizedUserProviderMock.Object,
        typeof(object));
    }

    [TestMethod]
    public async Task Test()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }
  }
}
