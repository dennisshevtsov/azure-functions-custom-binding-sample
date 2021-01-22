// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;
  
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.Binding.Request;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  /// <summary>Provides a simple API to handle an HTTP request.</summary>
  public static class GetProductFunction
  {
    /// <summary>Gets an instance of the <see cref="ProductDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.GetProductRequestDto"/> class represents.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents conditions to query products.</param>
    /// <param name="document">An object that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.GetProductRequestDto"/> class represents.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An instance of the <see cref="ProductDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.GetProductRequestDto"/> class represents.</returns>
    [FunctionName(nameof(GetProductFunction))]
    public static ProductDocument ExecuteAsync(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest httpRequest,
      [Request] GetProductRequestDto requestDto,
      [Document] ProductDocument document,
      CancellationToken cancellationToken) => document;
  }
}
