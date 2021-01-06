// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to Cosmos DB.</summary>
  public interface IDocumentClient
  {
    public Task<Document<TEntity>> FirstOrDefaultAsync<TEntity>(
      string id, string partitionKey, CancellationToken cancellationToken) where TEntity : class;

    public IAsyncEnumerable<Document<TEntity>> AsEnumerableAsync<TEntity>(
      string partitionId,
      string query,
      IDictionary<string, object> parameters,
      CancellationToken cancellationToken) where TEntity : class;

    public Task<Document<TEntity>> InsertAsync<TEntity>(Document<TEntity> document, CancellationToken cancellationToken) where TEntity : class;

    public Task<Document<TEntity>> UpdateAsync<TEntity>(Document<TEntity> document, CancellationToken cancellationToken) where TEntity : class;

    public Task DeleteAsync<TEntity>(Guid id, string partitionKey, CancellationToken cancellationToken) where TEntity : class;
  }
}
