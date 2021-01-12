// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding;

  public static class SearchOrderFunction
  {
    [FunctionName(nameof(SearchOrderFunction))]
    public static IEnumerable<OrderDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "order")] HttpRequest httpRequest,
      [Request] SearchOrdersRequestDto requestDto,
      [Entity] IEnumerable<OrderDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
