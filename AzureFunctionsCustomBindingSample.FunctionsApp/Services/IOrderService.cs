// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;

  public interface IOrderService
  {
    public Task<OrderEntity> CreateOrderAsync(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductEntity> productEntityDictionary,
      UserEntity userEntity,
      CancellationToken cancellationToken);
  }
}
