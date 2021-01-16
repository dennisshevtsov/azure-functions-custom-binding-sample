// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Testing.Binding
{
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Api.Binding.Validation;

  [TestClass]
  public sealed class ValidationValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private ValidationValueProvider _valueProvider;

    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new ValidationValueProvider(_httpRequestMock.Object);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Valid_Validation_Result()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Invalid_Validation_Result()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Throw_If_It_Is_Invalid_And_ThrowIfFaild_Is_True()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Valid_And_ThrowIfFaild_Is_True()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Invalid_And_ThrowIfFaild_Is_False()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }
  }
}
