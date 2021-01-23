﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationValueProvider : IValueProvider
  {
    private readonly IValidatorProvider _validatorProvider;
    private readonly HttpRequest _httpRequest;
    private readonly bool _throwIfInvalid;

    /// <summary>Initializes a new instance of the <see cref="ValidationValueProvider"/> class.</summary>
    /// <param name="validatorProvider"></param>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="throwIfInvalid">An object that indicates whether it should throw an exception if a result is invalid.</param>
    public ValidationValueProvider(
      IValidatorProvider validatorProvider,
      HttpRequest httpRequest,
      bool throwIfInvalid)
    {
      _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      _throwIfInvalid = throwIfInvalid;
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type => typeof(ValidationResult);

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
    {
      var validator = _validatorProvider.GetValidator(_httpRequest);

      if (validator == null)
      {
        throw new InvalidOperationException();
      }

      var errors = validator.Validate();
      var validationResult = new ValidationResult(errors);

      if (_throwIfInvalid && validationResult.IsValid)
      {
        throw new InvalidRequestException(validationResult.Errors);
      }

      return Task.FromResult<object>(validationResult);
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => ValidationBinding.ParameterDescriptorName;
  }
}