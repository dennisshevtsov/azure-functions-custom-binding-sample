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

  /// <summary>Provides a simple API to handle an HTTP request.</summary>
  public sealed class SearchProductsFunction
  {
    /// <summary>Gets a collection of instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.SearchProductsRequestDto"/> class represents.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents conditions to query products.</param>
    /// <param name="documents">An object that exposes the enumerator, which supports a simple iteration over a collection of a specified type. The type is the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that exposes the enumerator, which supports a simple iteration over a collection of a specified type. The type is the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class.</returns>
    [FunctionName(nameof(SearchProductsFunction))]
    public IEnumerable<ProductDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "product")] HttpRequest httpRequest,
      [Request] SearchProductsRequestDto requestDto,
      [Document] IEnumerable<ProductDocument> documents,
      CancellationToken cancellationToken) => documents;
  }
}
