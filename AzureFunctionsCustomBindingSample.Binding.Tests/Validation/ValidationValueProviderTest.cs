// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  [TestClass]
  public sealed class ValidationValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private ValidationValueProvider _valueProvider;

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Valid_Validation_Result()
    {
      Setup(false, Enumerable.Empty<string>());

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
      Setup(false, GetErrors());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var validationResult = value as ValidationResult;

      Assert.IsNotNull(validationResult);
      Assert.IsFalse(validationResult.IsValid);
      Assert.IsTrue(validationResult.Errors.Any());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidRequestException))]
    public async Task GetValueAsync_Should_Throw_If_It_Is_Invalid_And_ThrowIfInvalid_Is_True()
    {
      Setup(true, GetErrors());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Valid_And_ThrowIfInvalid_Is_True()
    {
      Setup(true, Enumerable.Empty<string>());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Not_Throw_If_It_Is_Invalid_And_ThrowIfInvalid_Is_False()
    {
      Setup(false, GetErrors());

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task GetValueAsync_Should_Throw_If_There_Is_No_Proper_Validator()
    {
      Setup(false);

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);
    }

    private void Setup(bool throwIfInvalid)
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new ValidationValueProvider(
          _httpRequestMock.Object, throwIfInvalid, typeof(TestValidator));
    }

    private void Setup(bool throwIfInvalid, IEnumerable<string> errors)
    {
      Setup(throwIfInvalid);

      var validatorMock = new Mock<IValidator>();

      validatorMock.Setup(validator => validator.Validate())
                   .Returns(errors);
    }

    private static IEnumerable<string> GetErrors()
    {
      yield return "error0";
      yield return "error1";
    }
  }
}
