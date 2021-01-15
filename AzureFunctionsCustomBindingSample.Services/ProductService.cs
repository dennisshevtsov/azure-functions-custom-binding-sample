// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  /// <summary>Provides a simple API to execute operation within instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class.</summary>
  public sealed class ProductService : IProductService
  {
    /// <summary>Creates a new product.</summary>
    /// <param name="command">An object that represents a command to create a product.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<ProductDocument> CreateProductAsync(CreateProductRequestDto command, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
