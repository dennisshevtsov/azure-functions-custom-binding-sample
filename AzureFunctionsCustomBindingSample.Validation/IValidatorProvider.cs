// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using Microsoft.AspNetCore.Http;

  public interface IValidatorProvider
  {
    public IValidator GetValidator(HttpRequest httpRequest);
  }
}
