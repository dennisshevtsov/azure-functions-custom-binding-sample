// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using System;

  public sealed class ProductUnitDocument
  {
    public Guid UnitId { get; set; }

    public string Name { get; set; }
  }
}
