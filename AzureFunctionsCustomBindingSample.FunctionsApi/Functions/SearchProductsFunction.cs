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
  
  public sealed class SearchProductsFunction
  {
    [FunctionName(nameof(SearchProductsFunction))]
    public IEnumerable<ProductDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "product")] HttpRequest httpRequest,
      [Request] SearchProductsRequestDto requestDto,
      [Entity] IEnumerable<ProductDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
