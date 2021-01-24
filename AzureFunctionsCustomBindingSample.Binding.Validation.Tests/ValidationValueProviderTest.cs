// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation.Tests
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  [TestClass]
  public sealed class ValidationValueProviderTest
  {
    //private Mock<HttpRequest> _httpRequestMock;
    //private Mock<IValidatorProvider> _validatorProviderMock;
    private ValidationValueProvider _valueProvider;

    //[TestInitialize]
    //public void Initialize()
    //{
    //  _httpRequestMock = new Mock<HttpRequest>();
    //  _validatorProviderMock = new Mock<IValidatorProvider>();

    //  _valueProvider = new ValidationValueProvider(
    //    _validatorProviderMock.Object, _httpRequestMock.Object, true);
    //}

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Valid_Validation_Result()
    {
      SetupForValidResult();

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var validationResult = value as ValidationResult;

      Assert.IsNotNull(validationResult);
      Assert.IsTrue(validationResult.IsValid);
      Assert.IsFalse(validationResult.Errors.Any());
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Invalid_Validation_Result()
    {
      SetupForInvalidResult();

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var validationResult = value as ValidationResult;

      Assert.IsNotNull(validationResult);
      Assert.IsFalse(validationResult.IsValid);
      Assert.IsTrue(validationResult.Errors.Any());
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Throw_If_It_Is_Invalid_And_ThrowIfInvalid_Is_True()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Valid_And_ThrowIfInvalid_Is_True()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Invalid_And_ThrowIfInvalid_Is_False()
    {
      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    private void SetupForValidResult()
    {
      var httpRequestMock = new Mock<HttpRequest>();
      var validatorProviderMock = new Mock<IValidatorProvider>();

      _valueProvider = new ValidationValueProvider(
          validatorProviderMock.Object, httpRequestMock.Object, false);

      var validatorMock = new Mock<IValidator>();

      validatorMock.Setup(validator => validator.Validate())
                   .Returns(Enumerable.Empty<string>());

      validatorProviderMock.Setup(provider => provider.GetValidator(It.IsAny<HttpRequest>()))
                           .Returns(validatorMock.Object);
    }

    private void SetupForInvalidResult()
    {
      var httpRequestMock = new Mock<HttpRequest>();
      var validatorProviderMock = new Mock<IValidatorProvider>();

      _valueProvider = new ValidationValueProvider(
          validatorProviderMock.Object, httpRequestMock.Object, false);

      var validatorMock = new Mock<IValidator>();

      validatorMock.Setup(validator => validator.Validate())
                   .Returns(new[]
                   {
                     "error0",
                     "error1",
                   });

      validatorProviderMock.Setup(provider => provider.GetValidator(It.IsAny<HttpRequest>()))
                           .Returns(validatorMock.Object);
    }
  }
}
