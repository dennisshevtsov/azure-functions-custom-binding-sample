// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Functions
{
  using System.Threading;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  public static class GetOrderFunction
  {
    [FunctionName(nameof(GetOrderFunction))]
    public static OrderDocument ExecuteAsync(
      GetOrderRequestDto request,
      OrderDocument document,
      CancellationToken cancellationToken) => document;
  }
}
