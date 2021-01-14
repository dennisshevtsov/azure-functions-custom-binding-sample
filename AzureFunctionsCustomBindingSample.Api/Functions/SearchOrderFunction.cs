// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Binding;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  
  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  public static class SearchOrderFunction
  {
    /// <summary></summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto"></param>
    /// <param name="documents"></param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that exposes the enumerator, which supports a simple iteration over a collection of a specified type. The type is the <see cref="AzureFunctionsCustomBindingSample.Documents.OrderDocument"/> class.</returns>
    [FunctionName(nameof(SearchOrderFunction))]
    public static IEnumerable<OrderDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "order")] HttpRequest httpRequest,
      [Request] SearchOrdersRequestDto requestDto,
      [Document] IEnumerable<OrderDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
