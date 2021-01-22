﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validators
{
  using System;
  using System.Collections.Generic;
  
  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Validation;

  /// <summary>Provides a simple API to validate an HTTP request.</summary>
  public sealed class CreateProductValidator : IValidator
  {
    private readonly HttpRequest _httpRequest;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Validators.CreateProductValidator"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public CreateProductValidator(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    /// <summary>Validates an HTTP request.</summary>
    /// <returns>An object that represents a collection of errors.</returns>
    public IEnumerable<string> Validate()
    {
      var requestDto = _httpRequest.HttpContext.Items["request-dto"] as CreateProductRequestDto;

      if (string.IsNullOrWhiteSpace(requestDto.Name))
      {
        yield return $"Property {nameof(CreateProductRequestDto.Name)} is required.";
      }

      if (requestDto.PricePerUnit <= 0)
      {
        yield return $"Property {nameof(CreateProductRequestDto.PricePerUnit)} must be > 0.";
      }

      var unitDocument = _httpRequest.HttpContext.Items["document"] as UnitDocument;

      if (unitDocument == null)
      {
        yield return $"Property {nameof(CreateProductRequestDto.Unit)} is invalid.";
      }
    }
  }
}