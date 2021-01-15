// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Binding;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  /// <summary>Provides a simple API to handle an HTTP request.</summary>
  public static class GetOrderFunction
  {
    /// <summary>Gets an instance of the <see cref="AzureFunctionsCustomBindingSample.Documents.OrderDocument"/> that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.GetOrderRequestDto"/> class represents.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents conditions to query an order.</param>
    /// <param name="document">An object that represents detail of an order.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Documents.OrderDocument"/> that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.GetOrderRequestDto"/> class represents.</returns>
    [FunctionName(nameof(GetOrderFunction))]
    public static OrderDocument ExecuteAsync(
      [HttpTrigger("get", Route = "order/{orderId}")] HttpRequest httpRequest,
      [Request] GetOrderRequestDto requestDto,
      [Document] OrderDocument document,
      CancellationToken cancellationToken) => document;
  }
}
