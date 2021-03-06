﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.CosmosDb
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Extends the API of the <see cref="IDocumentClient"/>.</summary>
  public static class DocumentClientExtensions
  {
    /// <summary>Receives documents from their persistence.</summary>
    /// <typeparam name="TDocument">A type of a document.</typeparam>
    /// <param name="partitionId">A value that represents a partition ID of a document.</param>
    /// <param name="query">A value that represents a condition to query documents.</param>
    /// <param name="parameters">An object that represents a collection of parameters for a query.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public static async Task<IList<TDocument>> ToListAsync<TDocument>(
      this IDocumentClient documentClient,
      string partitionId,
      string query,
      IReadOnlyDictionary<string, object> parameters,
      CancellationToken cancellationToken)
      where TDocument : DocumentBase
    {
      if (documentClient == null)
      {
        throw new ArgumentNullException(nameof(documentClient));
      }

      var documents = new List<TDocument>();

      await foreach (var document in documentClient.AsAsyncEnumerable<TDocument>(
        partitionId, query, parameters, null, cancellationToken))
      {
        documents.Add(document);
      }

      return documents;
    }

    /// <summary>Receives documents from their persistence.</summary>
    /// <typeparam name="TDocument">A type of a document.</typeparam>
    /// <param name="partitionId">A value that represents a partition ID of a document.</param>
    /// <param name="query">A value that represents a condition to query documents.</param>
    /// <param name="parameters">An object that represents a collection of parameters for a query.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public static async Task<IList<TDocument>> ToListAsync<TDocument>(
      this IDocumentClient documentClient,
      IDocumentCollectionQuery documentCollectionQuery,
      CancellationToken cancellationToken)
      where TDocument : DocumentBase
    {
      var documents = new List<TDocument>();

      await foreach (var document in documentClient.AsAsyncEnumerable<TDocument>(
        documentCollectionQuery, cancellationToken))
      {
        documents.Add(document);
      }

      return documents;
    }

    /// <summary>Receives documents from their persistence that are grouped by its ID.</summary>
    /// <typeparam name="TDocument">A type of a document.</typeparam>
    /// <param name="partitionId">A value that represents a partition ID of a document.</param>
    /// <param name="query">A value that represents a condition to query documents.</param>
    /// <param name="parameters">An object that represents a collection of parameters for a query.</param>
    /// <param name="continuationToken">An object that represents a continuation token in the Azure Cosmos DB service.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public static async Task<IDictionary<Guid, TDocument>> ToDictionaryAsync<TDocument>(
      this IDocumentClient documentClient,
      string partitionId,
      string query,
      IReadOnlyDictionary<string, object> parameters,
      string continuationToken,
      CancellationToken cancellationToken)
      where TDocument : DocumentBase
    {
      if (documentClient == null)
      {
        throw new ArgumentNullException(nameof(documentClient));
      }

      var documentDictionary = new Dictionary<Guid, TDocument>();

      await foreach (var document in documentClient.AsAsyncEnumerable<TDocument>(
        partitionId, query, parameters, continuationToken, cancellationToken))
      {
        documentDictionary.Add(document.Id, document);
      }

      return documentDictionary;
    }

    /// <summary>Receives a document from its persistence.</summary>
    /// <typeparam name="TDocument">A type of a document.</typeparam>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.CosmosDb.DocumentBase"/> class.</param>
    /// <param name="query">An object that represents conditions to query a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public static Task<TDocument> FirstOrDefaultAsync<TDocument>(
      this IDocumentClient documentClient,
      IDocumentQuery query,
      CancellationToken cancellationToken)
      where TDocument : DocumentBase
    {
      if (documentClient == null)
      {
        throw new ArgumentNullException(nameof(documentClient));
      }

      if (query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      return documentClient.FirstOrDefaultAsync<TDocument>(
        query.Id, query.PartitionId, cancellationToken);
    }

    /// <summary>Receives documents from their persistence.</summary>
    /// <typeparam name="TDocument">A type of a document.</typeparam>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.CosmosDb.DocumentBase"/> class.</param>
    /// <param name="query">An object that represents conditions to query a collection of documents.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public static IAsyncEnumerable<TDocument> AsAsyncEnumerable<TDocument>(
      this IDocumentClient documentClient,
      IDocumentCollectionQuery query,
      CancellationToken cancellationToken)
      where TDocument : DocumentBase
    {
      if (documentClient == null)
      {
        throw new ArgumentNullException(nameof(documentClient));
      }

      if (query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      return documentClient.AsAsyncEnumerable<TDocument>(
        query.PartitionId, query.Query, query.Parameters, query.ContinuationToken, cancellationToken);
    }
  }
}
