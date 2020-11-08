// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Entities
{
  using System;

  public sealed class OrderProductRelationEntity
  {
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int ProductCount { get; set; }
  }
}
