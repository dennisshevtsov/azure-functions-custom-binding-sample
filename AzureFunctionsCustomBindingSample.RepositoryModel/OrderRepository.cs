// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using AzureFunctionsCustomBindingSample.EntityModel;
  using Microsoft.Azure.Cosmos;
  using Microsoft.IO;

  public sealed class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
  {
    public OrderRepository(
      RequestOptions requestOptions,
      Container container,
      RecyclableMemoryStreamManager streamManager)
      : base(requestOptions, container, streamManager) { }
  }
}
