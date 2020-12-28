// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  public interface IRepository<TEntity> where TEntity : class
  {
    public Task<Document<TEntity>> GetAsync(string primaryKey, string partitionKey, CancellationToken cancellationToken);

    public IAsyncEnumerable<Document<TEntity>> AsEnumerableAsync(CancellationToken cancellationToken);

    public Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

    public Task UpdateAsync(Document<TEntity> document, CancellationToken cancellationToken);

    public Task DeleteAsync(Document<TEntity> document, CancellationToken cancellationToken);
  }
}
