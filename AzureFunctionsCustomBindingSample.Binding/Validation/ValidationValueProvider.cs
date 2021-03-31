// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Initializes a parameter that is marked with the <see cref="Binding.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly bool _throwIfInvalid;
    private readonly Type _validatorType;

    /// <summary>Initializes a new instance of the <see cref="ValidationValueProvider"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="throwIfInvalid">An object that indicates whether it should throw an exception if a result is invalid.</param>
    /// <param name="validatorType">An object that represents a type of a validator.</param>
    public ValidationValueProvider(
      HttpRequest httpRequest,
      bool throwIfInvalid,
      Type validatorType)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      _throwIfInvalid = throwIfInvalid;
      _validatorType = validatorType ?? throw new ArgumentNullException(nameof(validatorType));
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type => typeof(ValidationResult);

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
    {
      var validator = ActivatorUtilities.CreateInstance(
          _httpRequest.HttpContext.RequestServices,
          _validatorType,
          _httpRequest.HttpContext.Items["__request__"]) as IValidator;

      if (validator == null)
      {
        throw new InvalidOperationException();
      }

      var errors = validator.Validate();
      var validationResult = new ValidationResult(errors);

      if (_throwIfInvalid && !validationResult.IsValid)
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
