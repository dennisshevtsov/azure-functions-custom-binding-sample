// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.IO;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  public sealed class Serializer : ISerializer
  {
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public Serializer(JsonSerializerOptions jsonSerializerOptions)
      => _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));

    public Task SerializeAsync<TEntity>(Stream output, TEntity entity, CancellationToken cancellationToken) where TEntity : class
      => JsonSerializer.SerializeAsync(output, entity, _jsonSerializerOptions, cancellationToken);

    public ValueTask<TEntity> DeserializeAsync<TEntity>(Stream input, CancellationToken cancellationToken)
      => JsonSerializer.DeserializeAsync<TEntity>(input, _jsonSerializerOptions, cancellationToken);

    public static ISerializer Get()
    {
      var jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      jsonSerializerOptions.Converters.Add(new DocumentJsonConverterFactory());

      var serializer = new Serializer(jsonSerializerOptions);

      return serializer;
    }
  }
}
