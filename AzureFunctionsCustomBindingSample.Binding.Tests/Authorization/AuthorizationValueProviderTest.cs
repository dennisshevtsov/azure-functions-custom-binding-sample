// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization.Tests
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

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
      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(new Mock<HttpContext>().Object);
      _authorizedUserProviderMock = new Mock<IAuthorizedUserProvider>();

      _valueProvider = new AuthorizationValueProvider(
        _httpRequestMock.Object,
        CancellationToken.None,
        _authorizedUserProviderMock.Object,
        typeof(object));
    }

    [TestMethod]
    public async Task Test()
    {
      _authorizedUserProviderMock.Setup(provider => provider.GetAuthorizedUserAsync(It.IsAny<HttpRequest>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new object());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    [ExpectedException(typeof(UnauthorizedException))]
    public async Task GetValueAsync_Should_Throw_Exception_If_User_Is_Not_Found()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Authorized_User()
    {
      _authorizedUserProviderMock.Setup(provider => provider.GetAuthorizedUserAsync(It.IsAny<HttpRequest>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new object());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }
  }
}
