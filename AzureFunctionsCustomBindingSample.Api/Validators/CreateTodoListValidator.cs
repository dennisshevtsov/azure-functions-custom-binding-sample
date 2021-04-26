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
  public sealed class CreateTodoListValidator : IValidator
  {
    private readonly CreateTodoListRequestDto _requestDto;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Validators."/> class.</summary>
    /// <param name="requestDto">An object that represents data to create a TODO list.</param>
    public CreateTodoListValidator(CreateTodoListRequestDto requestDto)
      => _requestDto = requestDto ?? throw new ArgumentNullException(nameof(requestDto));

    /// <summary>Validates an HTTP request.</summary>
    /// <returns>An object that represents a collection of errors.</returns>
    public IEnumerable<string> Validate()
    {
      if (string.IsNullOrWhiteSpace(_requestDto.Title))
      {
        yield return $"The {nameof(CreateTodoListRequestDto.Title)} is required.";
      }
    }
  }
}
