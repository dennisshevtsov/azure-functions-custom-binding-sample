// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ProductServiceApi.Functions
{
  using System;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.ProductServiceApi.Documents;

  public sealed class SearchProductsFunction
  {
    [FunctionName(nameof(SearchProductsFunction))]
    public ProductDocument ExecuteAsync() => throw new NotImplementedException();
  }
}
