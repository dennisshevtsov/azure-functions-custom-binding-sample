// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Binding;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  public static class CreateOrderFunction
  {
    /// <summary></summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto"></param>
    /// <param name="productDocumentDictionary"></param>
    /// <param name="userDocument"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    [FunctionName(nameof(CreateOrderFunction))]
    public static async Task<OrderDocument> ExecuteAsync(
      [HttpTrigger("post", Route = "order")] HttpRequest httpRequest,
      [Request] CreateOrderRequestDto requestDto,
      [Document] IDictionary<Guid, ProductDocument> productDocumentDictionary,
      [Authorization] UserDocument userDocument,
      [Service] IOrderService service,
      CancellationToken cancellationToken)
      => await service.CreateOrderAsync(
        requestDto.Products, productDocumentDictionary, userDocument, cancellationToken);
  }
}
