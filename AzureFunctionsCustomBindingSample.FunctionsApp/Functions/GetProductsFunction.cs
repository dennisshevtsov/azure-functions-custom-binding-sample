// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Functions
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;

  public static class GetProductsFunction
  {
    [FunctionName(nameof(GetProductsFunction))]
    public static ProductEntity Execute(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest request,
      ProductEntity productEntity)
      => productEntity;
  }
}
