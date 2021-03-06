﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>Represents detail of an validation result.</summary>
  public sealed class ValidationResult
  {
    /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
    /// <param name="isValid">A value that indicates if data is valid.</param>
    public ValidationResult(IEnumerable<string> errors)
      => Errors = errors ?? throw new ArgumentNullException(nameof(errors));

    /// <summary>Gets a value that indicates if data is valid.</summary>
    public bool IsValid => !Errors.Any();

    /// <summary>Gets an object that represents a collection of errors.</summary>
    public IEnumerable<string> Errors { get; }
  }
}
