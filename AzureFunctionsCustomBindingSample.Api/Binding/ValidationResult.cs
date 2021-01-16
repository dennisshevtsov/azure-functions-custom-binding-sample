// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding
{
  /// <summary>Represents detail of an validation result.</summary>
  public sealed class ValidationResult
  {
    /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
    /// <param name="isValid">A value that indicates if data is valid.</param>
    public ValidationResult(bool isValid) => IsValid = isValid;

    /// <summary>Gets a value that indicates if data is valid.</summary>
    public bool IsValid { get; }
  }
}
