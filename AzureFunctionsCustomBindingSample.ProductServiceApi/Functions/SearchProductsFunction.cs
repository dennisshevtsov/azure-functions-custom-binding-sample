// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ProductServiceApi.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.ProductServiceApi.Documents;
  using AzureFunctionsCustomBindingSample.ProductServiceApi.Dtos;

  public sealed class SearchProductsFunction
  {
    [FunctionName(nameof(SearchProductsFunction))]
    public IEnumerable<ProductDocument> ExecuteAsync(
      SearchProductsRequestDto request,
      IEnumerable<ProductDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
