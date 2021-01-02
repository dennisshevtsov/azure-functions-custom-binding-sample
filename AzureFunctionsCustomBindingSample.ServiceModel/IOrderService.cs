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

  /// <summary>Provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.EntityModel.OrderEntity"/> class.</summary>
  public interface IOrderService
  {
    /// <summary>Creates a new order.</summary>
    /// <param name="products">An object that represents a collection of pairs: a product and quantity.</param>
    /// <param name="productEntityDictionary">An object that represents a collection of pairs: ID and a product. That exist in the DB.</param>
    /// <param name="userEntity">An object that represents a user that created an order.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<OrderEntity> CreateOrderAsync(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductEntity> productEntityDictionary,
      UserEntity userEntity,
      CancellationToken cancellationToken);
  }
}
