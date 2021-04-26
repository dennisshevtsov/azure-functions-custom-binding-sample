// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;

  /// <summary>Represents a simple API to query documents.</summary>
  public interface IDocumentProvider
  {
    /// <summary>Gets a single document for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="documentType">An object that represents a type of a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetDocumentAsync(HttpRequest httpRequest, Type documentType, CancellationToken cancellationToken);

    /// <summary>Gets a collection of documents for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="documentType">An object that represents a type of a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetDocumentsAsync(HttpRequest httpRequest, Type documentType, CancellationToken cancellationToken);
  }
}
