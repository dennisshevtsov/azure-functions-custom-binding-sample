// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using System;

  public sealed class OrderProductDocument
  {
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public float Units { get; set; }

    public ProductUnitDocument Unit { get; set; }

    public float PricePerUnit { get; set; }

    public float TotalPrice { get; set; }
  }
}
