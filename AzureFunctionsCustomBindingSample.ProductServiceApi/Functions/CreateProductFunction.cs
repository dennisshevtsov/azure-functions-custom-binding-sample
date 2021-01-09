// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ProductServiceApi.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.ProductServiceApi.Documents;
  using AzureFunctionsCustomBindingSample.ProductServiceApi.Dtos;
  using AzureFunctionsCustomBindingSample.ProductServiceApi.Services;

  public static class CreateProductFunction
  {
    [FunctionName(nameof(CreateProductFunction))]
    public static async Task<ProductDocument> ExecuteAsync(
      IProductService service,
      CreateProductRequestDto request,
      CancellationToken cancellationToken)
      => await service.CreateProductAsync(request, cancellationToken);
  }
}
