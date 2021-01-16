﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    /// <summary>Initializes a new instance of the <see cref="ValidationValueProvider"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public ValidationValueProvider(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type => typeof(ValidationResult);

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
    {
      var validatorProvider = _httpRequest.HttpContext
                                          .RequestServices
                                          .GetRequiredService<IValidatorProvider>();
      var validator = validatorProvider.GetValidator(_httpRequest);
      var errors = validator.Validate();
      var validationResult = new ValidationResult(errors);

      if (validationResult.IsValid)
      {
        throw new Exception();
      }

      return Task.FromResult<object>(validationResult);
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => ValidationBinding.ParameterDescriptorName;
  }
}
