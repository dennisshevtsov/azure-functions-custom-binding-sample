// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

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
