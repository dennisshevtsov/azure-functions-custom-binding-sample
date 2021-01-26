// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;
  using System.IO;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to serialize/deserialize objects.</summary>
  public sealed class Serializer : ISerializer
  {
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>Initializes a new instance of the <see cref="Serializer"/> class.</summary>
    /// <param name="jsonSerializerOptions">An object that provides options to be used with <see cref="System.Text.Json.JsonSerializer"/>.</param>
    public Serializer(JsonSerializerOptions jsonSerializerOptions)
      => _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));

    /// <summary>Serializes an object to a stream.</summary>
    /// <typeparam name="TDocument">A type of a serializing object.</typeparam>
    /// <param name="output">An object that represents an output stream.</param>
    /// <param name="document">An object that it is required to serialize.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task SerializeAsync<TDocument>(Stream output, TDocument document, CancellationToken cancellationToken) where TDocument : class
      => JsonSerializer.SerializeAsync(output, document, _jsonSerializerOptions, cancellationToken);

    /// <summary>Deserializes an object from a stream.</summary>
    /// <typeparam name="TDocument">A type of a serializing object.</typeparam>
    /// <param name="input">An object that represents an input stream.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public ValueTask<TDocument> DeserializeAsync<TDocument>(Stream input, CancellationToken cancellationToken) where TDocument : class
      => JsonSerializer.DeserializeAsync<TDocument>(input, _jsonSerializerOptions, cancellationToken);

    /// <summary>Creates an instance of the <see cref="ISerializer"/> type.</summary>
    /// <returns>An instance of the <see cref="ISerializer"/> type.</returns>
    public static ISerializer Get()
    {
      var jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
          new DocumentJsonConverterFactory(),
          new DocumentCollectionJsonConverterFactory(),
        },
      };

      var serializer = new Serializer(jsonSerializerOptions);

      return serializer;
    }
  }
}
