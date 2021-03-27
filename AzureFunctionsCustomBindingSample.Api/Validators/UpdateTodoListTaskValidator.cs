// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Validators
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Validation;

  public sealed class UpdateTodoListTaskValidator : IValidator
  {
    private readonly UpdateTodoListTaskRequestDto _requestDto;

    public UpdateTodoListTaskValidator(UpdateTodoListTaskRequestDto requestDto)
      => _requestDto = requestDto ?? throw new ArgumentNullException(nameof(requestDto));

    public IEnumerable<string> Validate()
    {
      if (string.IsNullOrWhiteSpace(_requestDto.Title))
      {
        yield return $"The {nameof(UpdateTodoListTaskRequestDto.Title)} is required.";
      }

      if (string.IsNullOrWhiteSpace(_requestDto.Description))
      {
        yield return $"The {nameof(UpdateTodoListTaskRequestDto.Description)} is required.";
      }

      if (_requestDto.Deadline == default)
      {
        yield return $"The {nameof(UpdateTodoListTaskRequestDto.Deadline)} is required.";
      }
    }
  }
}
