// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Binding;

  public static class GetOrderFunction
  {
    [FunctionName(nameof(GetOrderFunction))]
    public static OrderDocument ExecuteAsync(
      [HttpTrigger("get", Route = "order/{orderId}")] HttpRequest httpRequest,
      [Request] GetOrderRequestDto requestDto,
      [Document] OrderDocument document,
      CancellationToken cancellationToken) => document;
  }
}
