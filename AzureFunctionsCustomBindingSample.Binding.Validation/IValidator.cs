// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System.Collections.Generic;

  /// <summary>Provides a simple API to validate an HTTP request.</summary>
  public interface IValidator
  {
    /// <summary>Validates an HTTP request.</summary>
    /// <returns>An object that represents a collection of errors.</returns>
    public IEnumerable<string> Validate();
  }
}
