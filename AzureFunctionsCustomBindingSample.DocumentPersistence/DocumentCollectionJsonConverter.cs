// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  /// <summary>Converts an object or value to or from JSON.</summary>
  /// <typeparam name="TDocument">The type of object or value handled by the converter.</typeparam>
  public sealed class DocumentCollectionJsonConverter<TDocument> : JsonConverter<DocumentCollection<TDocument>> where TDocument : DocumentBase
  {
    private const string DocumentsPropertyName = "Documents";
    private const string ResourceIdPropertyName = "_rid";
    private const string CountPropertyName = "_count";

    /// <summary>Reads and converts the JSON to type T.</summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The converted value.</returns>
    public override DocumentCollection<TDocument> Read(
      ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var documents = new List<TDocument>();
      var documentCollection = new DocumentCollection<TDocument>
      {
        Documents = documents,
      };

      string propertyName = null;
      int braces = 0;

      do
      {
        if (reader.TokenType == JsonTokenType.PropertyName)
        {
          propertyName = reader.GetString();
        }
        else if (reader.TokenType == JsonTokenType.String &&
                 propertyName == DocumentCollectionJsonConverter<TDocument>.ResourceIdPropertyName)
        {
          documentCollection.ResourceId = reader.GetString();
        }
        else if (reader.TokenType == JsonTokenType.Number &&
                 propertyName == DocumentCollectionJsonConverter<TDocument>.CountPropertyName)
        {
          documentCollection.Count = reader.GetInt32();
        }
        else if (propertyName == DocumentCollectionJsonConverter<TDocument>.DocumentsPropertyName &&
                 reader.TokenType != JsonTokenType.StartArray &&
                 reader.TokenType != JsonTokenType.EndArray)
        {
          documents.Add(ReadDocument(ref reader, options));
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
          ++braces;
        }
        else if (reader.TokenType == JsonTokenType.EndObject)
        {
          --braces;
        }
      }
      while (reader.Read() && braces != 0);

      return documentCollection;
    }

    /// <summary>Writes a specified value as JSON.</summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="document">The value to convert to JSON.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(
      Utf8JsonWriter writer, DocumentCollection<TDocument> value, JsonSerializerOptions options)
      => throw new NotSupportedException();

    private static TDocument ReadDocument(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
      if (options.GetConverter(typeof(TDocument)) is JsonConverter<TDocument> converter)
      {
        return converter.Read(ref reader, typeof(TDocument), options);
      }

      return JsonSerializer.Deserialize<TDocument>(ref reader, options);
    }
  }
}
