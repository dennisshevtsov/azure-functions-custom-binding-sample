// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to serialize/deserialize objects.</summary>
  public interface ISerializer
  {
    public Task SerializeAsync<TEntity>(Stream output, TEntity entity, CancellationToken cancellationToken) where TEntity : class;

    public ValueTask<TEntity> DeserializeAsync<TEntity>(Stream input, CancellationToken cancellationToken) where TEntity : class;
  }
}
