// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation.Tests
{
  using System.Collections.Generic;
  using System.Linq;

  public sealed class TestValidator : IValidator
  {
    public IEnumerable<string> Validate() => Enumerable.Empty<string>();
  }
}
