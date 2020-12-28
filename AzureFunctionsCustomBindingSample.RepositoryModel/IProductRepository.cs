// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.EntityModel;

  public interface IProductRepository
  {
    public Task<ProductEntity> GetProductAsync(Guid productId, CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsAsync(IEnumerable<Guid> products, CancellationToken cancellationToken);
  }
}
