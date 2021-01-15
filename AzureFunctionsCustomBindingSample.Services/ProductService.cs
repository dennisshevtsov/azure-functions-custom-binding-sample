// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  /// <summary>Provides a simple API to execute operation within instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.ProductDocument"/> class.</summary>
  public sealed class ProductService : IProductService
  {
    private readonly IDocumentClient _documentClient;

    /// <summary>Initializes a new instance of the <see cref="ProductService"/> class.</summary>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.DocumentPersistence.DocumentBase"/> class.</param>
    public ProductService(IDocumentClient documentClient)
    {
      _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
    }

    /// <summary>Creates a new product.</summary>
    /// <param name="command">An object that represents a command to create a product.</param>
    /// <param name="unitDocument">An object of the <see cref="AzureFunctionsCustomBindingSample.Documents.UnitDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Dtos.CreateProductRequestDto"/> class represents.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<ProductDocument> CreateProductAsync(
      CreateProductRequestDto command, UnitDocument unitDocument, CancellationToken cancellationToken)
    {
      var sku = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 5);
      var productDocument = ProductDocument.New(sku, command.Name, command.Description, unitDocument);

      return _documentClient.InsertAsync(productDocument, cancellationToken);
    }
  }
}
