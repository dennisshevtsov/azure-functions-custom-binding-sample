// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Binding;
  
  public sealed class SearchProductsFunction
  {
    [FunctionName(nameof(SearchProductsFunction))]
    public IEnumerable<ProductDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "product")] HttpRequest httpRequest,
      [Request] SearchProductsRequestDto requestDto,
      [Document] IEnumerable<ProductDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
