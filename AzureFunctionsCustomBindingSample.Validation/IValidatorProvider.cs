// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using Microsoft.AspNetCore.Http;

  /// <summary>Provides a simple API to get an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</summary>
  public interface IValidatorProvider
  {
    /// <summary>Gets an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</returns>
    public IValidator GetValidator(HttpRequest httpRequest);
  }
}
