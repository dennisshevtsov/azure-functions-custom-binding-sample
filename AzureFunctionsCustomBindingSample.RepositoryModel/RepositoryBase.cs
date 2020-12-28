// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
  {
    public Task<Document<TEntity>> GetAsync(string primaryKey, string partitionKey, CancellationToken cancellationToken) => throw new NotImplementedException();

    public IAsyncEnumerable<Document<TEntity>> AsEnumerableAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task InsertAsync(TEntity entity, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateAsync(Document<TEntity> document, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteAsync(Document<TEntity> document, CancellationToken cancellationToken) => throw new NotImplementedException();
  }
}
