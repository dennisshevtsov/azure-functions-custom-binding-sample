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

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  public static class GetOrderFunction
  {
    /// <summary></summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto"></param>
    /// <param name="document"></param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    [FunctionName(nameof(GetOrderFunction))]
    public static OrderDocument ExecuteAsync(
      [HttpTrigger("get", Route = "order/{orderId}")] HttpRequest httpRequest,
      [Request] GetOrderRequestDto requestDto,
      [Document] OrderDocument document,
      CancellationToken cancellationToken) => document;
  }
}
