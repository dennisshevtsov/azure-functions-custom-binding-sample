// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.EntityModel
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public sealed class OrderEntity
  {
    public Guid OrderId { get; set; }

    public DateTime CreatedOn { get; set; }

    public OrderUserRelationEntity CreatedBy { get; set; }

    public IEnumerable<OrderProductRelationEntity> Products { get; set; }

    public static OrderEntity New(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductEntity> productEntityDictionary,
      UserEntity userEntity)
      => new OrderEntity
      {
        OrderId = Guid.NewGuid(),
        CreatedOn = DateTime.UtcNow,
        CreatedBy = new OrderUserRelationEntity
        {
          UserId = userEntity.UserId,
          Name = userEntity.Name,
          Address = userEntity.Address,
          Phone = userEntity.Phone,
          Email = userEntity.Email,
        },
        Products = productEntityDictionary.Select(productPair => new OrderProductRelationEntity
        {
          ProductId = productPair.Key,
          Name = productPair.Value.Name,
          Units = products[productPair.Key],
          PricePerUnit = productPair.Value.Price,
          Price = products[productPair.Key] * productPair.Value.Price,
        }),
      };
  }
}
