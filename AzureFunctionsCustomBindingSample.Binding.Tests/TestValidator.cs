﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Tests
{
  using System.Collections.Generic;
  using System.Linq;

  using AzureFunctionsCustomBindingSample.Binding.Validation;

  public sealed class TestValidator : IValidator
  {
    public IEnumerable<string> Validate() => Enumerable.Empty<string>();
  }
}