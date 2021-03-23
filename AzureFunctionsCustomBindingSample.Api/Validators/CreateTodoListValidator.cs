// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Validators
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Validation;

  public sealed class CreateTodoListValidator : IValidator
  {
    private readonly CreateTodoListRequestDto _requestDto;

    public CreateTodoListValidator(CreateTodoListRequestDto requestDto)
      => _requestDto = requestDto ?? throw new ArgumentNullException(nameof(requestDto));

    public IEnumerable<string> Validate()
    {
      if (string.IsNullOrWhiteSpace(_requestDto.Title))
      {
        yield return $"The {nameof(CreateTodoListRequestDto.Title)} is required.";
      }
    }
  }
}
