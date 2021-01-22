// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json;

  /// <summary>Represents detail of an exception that occurs if a validation is faild.</summary>
  public sealed class InvalidRequestException : Exception
  {
    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.InvalidRequestException"/> class.</summary>
    /// <param name="errors">An object that represents a collection of validation errors.</param>
    public InvalidRequestException(IEnumerable<string> errors)
      => Errors = errors ?? throw new ArgumentNullException(nameof(errors));

    /// <summary>Gets/set an object that represents a collection of validation errors.</summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>Gets/sets a value that represents an error message.</summary>
    public override string Message => JsonSerializer.Serialize(
      new
      {
        message = "The validation is faild.",
        error = Errors,
      },
      new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      });
  }
}
