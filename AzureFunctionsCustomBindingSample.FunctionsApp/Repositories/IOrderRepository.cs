// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Repositories
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;

  public interface IOrderRepository
  {
    public Task<OrderEntity> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);

    public Task<OrderEntity> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken);
  }
}
