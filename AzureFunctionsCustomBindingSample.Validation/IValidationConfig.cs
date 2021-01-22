// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  /// <summary>Provides a simple API to register a validator.</summary>
  public interface IValidationConfig
  {
    /// <summary>Registers a validator.</summary>
    /// <typeparam name="TValidator">A type of a validator.</typeparam>
    /// <param name="uri">A value that represents a URI of an HTTP request.</param>
    /// <param name="method">A value that represents a type of an HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidationConfig"/>.</returns>
    public IValidationConfig AddValidator<TValidator>(string uri, string method) where TValidator : IValidator;
  }
}
