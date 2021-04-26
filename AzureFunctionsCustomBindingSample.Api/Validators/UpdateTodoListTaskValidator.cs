// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Validators
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Validation;

  /// <summary>Provides a simple API to validate an HTTP request.</summary>
  public sealed class UpdateTodoListTaskValidator : IValidator
  {
    private readonly UpdateTodoListTaskRequestDto _requestDto;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Validators."/> class.</summary>
    /// <param name="requestDto">An object that represents data to a task of a TODO list.</param>
    public UpdateTodoListTaskValidator(UpdateTodoListTaskRequestDto requestDto)
      => _requestDto = requestDto ?? throw new ArgumentNullException(nameof(requestDto));

    /// <summary>Validates an HTTP request.</summary>
    /// <returns>An object that represents a collection of errors.</returns>
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
