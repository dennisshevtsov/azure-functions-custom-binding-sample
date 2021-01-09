// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ProductServiceApi.Services
{
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.ProductServiceApi.Documents;
  using AzureFunctionsCustomBindingSample.ProductServiceApi.Dtos;

  public interface IProductService
  {
    public Task<ProductDocument> CreateProductAsync(CreateProductRequestDto command, CancellationToken cancellationToken);
  }
}
