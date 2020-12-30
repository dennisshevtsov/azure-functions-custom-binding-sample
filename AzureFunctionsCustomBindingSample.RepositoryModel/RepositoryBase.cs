// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.Runtime.CompilerServices;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.Cosmos;
  using Microsoft.IO;

  public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
  {
    private readonly RequestOptions _requestOptions;
    private readonly Container _container;
    private readonly RecyclableMemoryStreamManager _streamManager;

    protected RepositoryBase(
      RequestOptions requestOptions,
      Container container,
      RecyclableMemoryStreamManager streamManager)
    {
      _requestOptions = requestOptions ?? throw new ArgumentNullException(nameof(requestOptions));
      _container = container ?? throw new ArgumentNullException(nameof(container));
      _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));
    }

    public Task<Document<TEntity>> FirstOrDefaultAsync(string primaryKey, string partitionKey, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async IAsyncEnumerable<Document<TEntity>> AsEnumerableAsync(
      string partitionId,
      string query,
      IDictionary<string, object> parameters,
      [EnumeratorCancellation] CancellationToken cancellationToken)
    {
      var queryDefinition = new QueryDefinition(query);

      foreach (var parameter in parameters)
      {
        queryDefinition.WithParameter(parameter.Key, parameter.Value);
      }

      var feedIterator = _container.GetItemQueryStreamIterator(
        queryDefinition,
        null,
        new QueryRequestOptions
        {
          PartitionKey = new PartitionKey(partitionId),
          MaxItemCount = _requestOptions.ItemsPerRequest,
        });

      while (feedIterator.HasMoreResults)
      {
        using (var responseMessage = await feedIterator.ReadNextAsync(cancellationToken))
        {
          responseMessage.EnsureSuccessStatusCode();

          var documentResponse = await JsonSerializer.DeserializeAsync<DocumentResponse<TEntity>>(
            responseMessage.Content,
            new JsonSerializerOptions(),
            cancellationToken);

          foreach (var document in documentResponse.Documents)
          {
            yield return document;
          }
        }
      }
    }

    public async Task InsertAsync(TEntity entity, string partitionKey, CancellationToken cancellationToken)
    {
      using (var stream = _streamManager.GetStream())
      {
        await JsonSerializer.SerializeAsync(stream, entity, null, new JsonSerializerOptions(), cancellationToken);
        var responseMessage = await _container.CreateItemStreamAsync(stream, new PartitionKey(partitionKey), null, cancellationToken);

        responseMessage.EnsureSuccessStatusCode();
      }
    }

    public async Task UpdateAsync(Document<TEntity> document, string id, string partitionKey, CancellationToken cancellationToken)
    {
      using (var stream = _streamManager.GetStream())
      {
        await JsonSerializer.SerializeAsync(stream, document, null, new JsonSerializerOptions(), cancellationToken);

        using (var responseMessage = await _container.ReplaceItemStreamAsync(stream, id, new PartitionKey(partitionKey), null, cancellationToken))
        {
          responseMessage.EnsureSuccessStatusCode();
        }
      }
    }

    public async Task DeleteAsync(Document<TEntity> document, string id, string partitionKey, CancellationToken cancellationToken)
    {
      using (var responseMessage = await _container.DeleteItemStreamAsync(id, new PartitionKey(partitionKey), null, cancellationToken))
      {
        responseMessage.EnsureSuccessStatusCode();
      }
    }
  }
}
