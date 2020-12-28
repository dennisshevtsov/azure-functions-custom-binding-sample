// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.EntityModel
{
  using System;

  public sealed class OrderProductRelationEntity
  {
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public float PricePerUnit { get; set; }

    public float Price { get; set; }

    public int Units { get; set; }
  }
}
