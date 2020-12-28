// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Functions
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.EntityModel;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Binding;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Dtos;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Enums;
  using AzureFunctionsCustomBindingSample.ServiceModel;

  public static class CreateOrderFunction
  {
    [FunctionName(nameof(CreateOrderFunction))]
    public static async Task<OrderEntity> ExecuteAsync(
      [HttpTrigger("post", Route = "order")] HttpRequest request,
      [Request] CreateOrderRequestDto requestDto,
      [Entity] IDictionary<Guid, ProductEntity> productEntityDictionary,
      [Validation] ValidationResult validationResult,
      [Authorization(Permission = Permission.Administrator)] UserEntity userEntity,
      [Service] IOrderService orderService,
      CancellationToken cancellationToken)
      => await orderService.CreateOrderAsync(
        requestDto.Products, productEntityDictionary, userEntity, cancellationToken);
  }
}
