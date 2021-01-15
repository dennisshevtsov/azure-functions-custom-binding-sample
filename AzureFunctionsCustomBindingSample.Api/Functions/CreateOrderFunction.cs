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

  /// <summary>Provides a simple API to handle an HTTP request.</summary>
  public static class CreateOrderFunction
  {
    /// <summary>Creates a new order.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents a command to create an order.</param>
    /// <param name="productDocumentDictionary">An object that represents a dictionary of instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> classes that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.CreateOrderRequestDto"/> class represents.</param>
    /// <param name="userDocument">An object that represents an authorized user.</param>
    /// <param name="service">An object that provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.OrderDocument"/> class.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
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
