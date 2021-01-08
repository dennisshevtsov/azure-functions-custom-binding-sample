// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.OrderServiceApi.Dtos
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.OrderServiceApi.Queries;
  
  public sealed class CreateOrderRequestDto : IGetProductsQuery
  {
    public IDictionary<Guid, int> Products { get; set; }

    IEnumerable<Guid> IGetProductsQuery.Products => Products.Keys;
  }
}
