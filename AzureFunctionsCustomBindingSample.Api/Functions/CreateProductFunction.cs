// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;
  using AzureFunctionsCustomBindingSample.Api.Binding;
  
  public static class CreateProductFunction
  {
    [FunctionName(nameof(CreateProductFunction))]
    public static async Task<ProductDocument> ExecuteAsync(
      [HttpTrigger("post", Route = "product")] HttpRequest httpRequest,
      [Request] CreateProductRequestDto request,
      [Service] IProductService service,
      CancellationToken cancellationToken)
      => await service.CreateProductAsync(request, cancellationToken);
  }
}
