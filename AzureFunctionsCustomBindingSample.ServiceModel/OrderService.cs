// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ServiceModel
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.EntityModel;
  using AzureFunctionsCustomBindingSample.RepositoryModel;

  public sealed class OrderService : IOrderService
  {
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<OrderEntity> CreateOrderAsync(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductEntity> productEntityDictionary,
      UserEntity userEntity,
      CancellationToken cancellationToken)
    {
      var orderEntity = OrderEntity.New(products, productEntityDictionary, userEntity);

      await _orderRepository.InsertAsync(orderEntity, cancellationToken);

      return orderEntity;
    }
  }
}
