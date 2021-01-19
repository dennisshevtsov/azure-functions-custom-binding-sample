// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>Represents detail of an validation result.</summary>
  public sealed class ValidationResult
  {
    /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
    /// <param name="isValid">A value that indicates if data is valid.</param>
    public ValidationResult(IEnumerable<string> errors) => Errors = errors;

    /// <summary>Gets a value that indicates if data is valid.</summary>
    public bool IsValid => Errors != null && Errors.Any();

    /// <summary>Gets an object that represents a collection of errors.</summary>
    public IEnumerable<string> Errors { get; }
  }
}
