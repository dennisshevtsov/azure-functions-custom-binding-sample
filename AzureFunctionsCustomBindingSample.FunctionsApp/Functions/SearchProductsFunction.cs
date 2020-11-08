// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Functions
{
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;

  public static class SearchProductsFunction
  {
    [FunctionName(nameof(SearchProductsFunction))]
    public static IEnumerable<ProductEntity> Execute(
      [HttpTrigger("get", Route = "product")] HttpRequest request,
      IEnumerable<ProductEntity> productEntities)
      => productEntities;
  }
}
