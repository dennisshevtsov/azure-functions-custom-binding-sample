// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System;
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

  public static class CreateOrderFunction
  {
    [FunctionName(nameof(CreateOrderFunction))]
    public static OrderDocument ExecuteAsync(
      CreateOrderRequestDto request,
      IDictionary<Guid, ProductDocument> productDocumentDictionary,
      IOrderService service,
      CancellationToken cancellationToken)
      => service.CreateOrderAsync(request.Products, productDocumentDictionary, cancellationToken);
  }
}
