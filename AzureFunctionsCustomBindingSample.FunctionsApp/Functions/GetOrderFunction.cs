// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Functions
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;

  public static class GetOrderFunction
  {
    [FunctionName(nameof(CreateOrderFunction))]
    public static OrderEntity Execute(
      [HttpTrigger("get", Route = "order/{orderId}")] HttpRequest request,
      OrderEntity orderEntity)
      => orderEntity;
  }
}
