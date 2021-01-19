// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Validators
{
  using System;
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Validation;

  public sealed class CreateProductValidator : IValidator
  {
    private readonly HttpRequest _httpRequest;

    public CreateProductValidator(HttpRequest httpRequest)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public IEnumerable<string> Validate()
    {
      var requestDto = _httpRequest.HttpContext.Items["request-dto"]as CreateProductRequestDto;

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
