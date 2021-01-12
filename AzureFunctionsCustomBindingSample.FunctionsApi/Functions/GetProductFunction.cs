// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding;

  public static class GetProductFunction
  {
    [FunctionName(nameof(GetProductFunction))]
    public static ProductDocument ExecuteAsync(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest httpRequest,
      [Request] GetProductRequestDto requestDto,
      [Entity] ProductDocument document,
      CancellationToken cancellationToken) => document;
  }
}
