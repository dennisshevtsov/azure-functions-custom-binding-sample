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

  using AzureFunctionsCustomBindingSample.FunctionsApp.Dtos;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Entities;
  using AzureFunctionsCustomBindingSample.FunctionsApp.Services;

  public static class CreateOrderFunction
  {
    [FunctionName(nameof(CreateOrderFunction))]
    public static async Task<OrderEntity> ExecuteAsync(
      [HttpTrigger("post", Route = "order")] HttpRequest request,
      [LoadFromRequest] CreateOrderRequestDto requestDto,
      [LoadForRequest<CreateOrderRequestDto>(request => request.Products.Keys)] IDictionary<Guid, ProductEntity> productEntityDictionary,
      [LoadForAuthorization] UserEntity userEntity,
      [LoadFromService] IOrderService orderService,
      [ValidateRequest] ValidationResult validationResult,
      [AuthorizeRequest] AuthorizationResult authorizationResult,
      CancellationToken cancellationToken)
      => await orderService.CreateOrderAsync(
        requestDto.Products, productEntityDictionary, userEntity, cancellationToken);
  }
}
