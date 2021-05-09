// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Threading.Tasks;
  using AzureFunctionsCustomBindingSample.Binding.Request;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Initializes a parameter that is marked with the <see cref="Binding.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationValueProvider : IValueProvider
  {
    private readonly HttpContext _httpContext;
    private readonly ObjectFactory _validatorFactory;
    private readonly bool _throwIfInvalid;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationValueProvider"/> class.</summary>
    /// <param name="httpContext">An object that encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="validatorFactory">An object that provides a simple API to create a proper validator.</param>
    /// <param name="throwIfInvalid">An object that indicates whether it should throw an exception if a result is invalid.</param>
    public ValidationValueProvider(
      HttpContext httpContext,
      ObjectFactory validatorFactory,
      bool throwIfInvalid)
    {
      _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
      _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
      _throwIfInvalid = throwIfInvalid;
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type => typeof(ValidationResult);

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
    {
      var validator = _validatorFactory.Invoke(
        _httpContext.RequestServices,
        new[]
        {
          _httpContext.Request.GetRequestDto(),
        }) as IValidator;
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
