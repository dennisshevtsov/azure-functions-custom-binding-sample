// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Binding;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  public static class CreateProductFunction
  {
    /// <summary></summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="request"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    [FunctionName(nameof(CreateProductFunction))]
    public static async Task<ProductDocument> ExecuteAsync(
      [HttpTrigger("post", Route = "product")] HttpRequest httpRequest,
      [Request] CreateProductRequestDto request,
      [Service] IProductService service,
      CancellationToken cancellationToken)
      => await service.CreateProductAsync(request, cancellationToken);
  }
}
