// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validators
{
  using System;
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Binding.Validation;
  using AzureFunctionsCustomBindingSample.Dtos;

  /// <summary>Provides a simple API to validate an HTTP request.</summary>
  public sealed class CreateOrderValidator : IValidator
  {
    private readonly HttpRequest _httpRequest;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Validators.CreateOrderValidator"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public CreateOrderValidator(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    /// <summary>Validates an HTTP request.</summary>
    /// <returns>An object that represents a collection of errors.</returns>
    public IEnumerable<string> Validate()
    {
      var requestDto = _httpRequest.HttpContext.Items["__request__"] as CreateOrderRequestDto;

      if (requestDto.Products == null &&
          requestDto.Products.Count == 0)
      {
        yield return $"Property {nameof(CreateOrderRequestDto.Products)} is required.";
      }
    }
  }
}
