// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Functions
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.EntityModel;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Binding;

  public static class GetOrderFunction
  {
    [FunctionName(nameof(GetOrderFunction))]
    public static OrderEntity Execute(
      [HttpTrigger("get", Route = "order/{orderId}")] HttpRequest request,
      [Entity] OrderEntity orderEntity)
      => orderEntity;
  }
}
