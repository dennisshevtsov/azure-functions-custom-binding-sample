// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  public static class SearchOrderFunction
  {
    [FunctionName(nameof(SearchOrderFunction))]
    public static IEnumerable<OrderDocument> ExecuteAsync(
      SearchOrdersRequestDto request,
      IEnumerable<OrderDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
