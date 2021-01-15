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

  /// <summary>Provides a simple API to handle an HTTP request.</summary>
  public static class CreateProductFunction
  {
    /// <summary>Creates a new product.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="request">An object that represents a command to create a product.</param>
    /// <param name="unitDocument">An object of the <see cref="AzureFunctionsCustomBindingSample.Documents.UnitDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.CreateProductRequestDto"/> class represents.</param>
    /// <param name="service">An object that provides a simple API to execute operation within instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    [FunctionName(nameof(CreateProductFunction))]
    public static async Task<ProductDocument> ExecuteAsync(
      [HttpTrigger("post", Route = "product")] HttpRequest httpRequest,
      [Request] CreateProductRequestDto request,
      [Document] UnitDocument unitDocument,
      [Service] IProductService service,
      CancellationToken cancellationToken)
      => await service.CreateProductAsync(request, unitDocument, cancellationToken);
  }
}
