// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.Cosmos;
  using Microsoft.Extensions.Options;
  using Microsoft.IO;

  /// <summary>Provides a simple API to Cosmos DB.</summary>
  public sealed class DocumentClient : IDocumentClient
  {
    private readonly DocumentClientOptions _requestOptions;
    private readonly Container _container;
    private readonly RecyclableMemoryStreamManager _streamManager;

    /// <summary>Initializes a new instance of the <see cref="DocumentClient"/> class.</summary>
    /// <param name="requestOptions">An object that represents options of a request to Cosmos DB.</param>
    /// <param name="container">An object that provides operations for reading, replacing, or deleting a specific, existing container or item in a container by ID.</param>
    /// <param name="streamManager">An object that manages pools of RecyclableMemoryStream objects.</param>
    public DocumentClient(
      IOptions<DocumentClientOptions> requestOptions,
      Container container,
      RecyclableMemoryStreamManager streamManager)
    {
      _requestOptions = requestOptions?.Value ?? throw new ArgumentNullException(nameof(requestOptions));
      _container = container ?? throw new ArgumentNullException(nameof(container));
      _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));
    }

    public async Task<Document<TEntity>> FirstOrDefaultAsync<TEntity>(
      string id, string partitionKey, CancellationToken cancellationToken) where TEntity : class
    {
      using (var responseMessage = await _container.ReadItemStreamAsync(id, new PartitionKey(partitionKey), null, cancellationToken))
      {
        responseMessage.EnsureSuccessStatusCode();

        var documentResponse = await JsonSerializer.DeserializeAsync<DocumentResponse<TEntity>>(
          responseMessage.Content,
          new JsonSerializerOptions(),
          cancellationToken);

        return documentResponse.Documents.FirstOrDefault();
      }
    }

    public async IAsyncEnumerable<Document<TEntity>> AsEnumerableAsync<TEntity>(
      string partitionId,
      string query,
      IDictionary<string, object> parameters,
      [EnumeratorCancellation] CancellationToken cancellationToken) where TEntity : class
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

    public async Task<Document<TEntity>> InsertAsync<TEntity>(
      TEntity entity, string partitionKey, CancellationToken cancellationToken) where TEntity : class
    {
      using (var stream = _streamManager.GetStream())
      {
        var creatingDocument = new Document<TEntity> { Id = Guid.NewGuid().ToString(), Entity = entity, };

        var jsonSerializerOptions = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        jsonSerializerOptions.Converters.Add(new DocumentJsonConverterFactory());

        await JsonSerializer.SerializeAsync(stream, creatingDocument, jsonSerializerOptions, cancellationToken);
        stream.Seek(0, SeekOrigin.Begin);

        using (var responseMessage = await _container.CreateItemStreamAsync(stream, new PartitionKey(partitionKey), null, cancellationToken))
        {
          responseMessage.EnsureSuccessStatusCode();

          var createdDocument = await JsonSerializer.DeserializeAsync<Document<TEntity>>(
            responseMessage.Content,
            jsonSerializerOptions,
            cancellationToken);

          return createdDocument;
        }
      }
    }

    public async Task<Document<TEntity>> UpdateAsync<TEntity>(
      Document<TEntity> document, string id, string partitionKey, CancellationToken cancellationToken) where TEntity : class
    {
      using (var stream = _streamManager.GetStream())
      {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        jsonSerializerOptions.Converters.Add(new DocumentJsonConverterFactory());
        await JsonSerializer.SerializeAsync(stream, document, null, new JsonSerializerOptions(), cancellationToken);
        stream.Seek(0, SeekOrigin.Begin);

        using (var responseMessage = await _container.ReplaceItemStreamAsync(stream, id, new PartitionKey(partitionKey), null, cancellationToken))
        {
          string requestBody = await new StreamReader(responseMessage.Content).ReadToEndAsync();

          responseMessage.EnsureSuccessStatusCode();

          var documentResponse = await JsonSerializer.DeserializeAsync<DocumentResponse<TEntity>>(
            responseMessage.Content,
            new JsonSerializerOptions(),
            cancellationToken);

          return documentResponse.Documents.FirstOrDefault();
        }
      }
    }

    public async Task DeleteAsync<TEntity>(string id, string partitionKey, CancellationToken cancellationToken) where TEntity : class
    {
      using (var responseMessage = await _container.DeleteItemStreamAsync(id, new PartitionKey(partitionKey), null, cancellationToken))
      {
        responseMessage.EnsureSuccessStatusCode();
      }
    }
  }
}
