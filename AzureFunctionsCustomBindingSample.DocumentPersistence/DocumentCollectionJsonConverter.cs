// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  /// <summary>Converts an object or value to or from JSON.</summary>
  /// <typeparam name="TDocument">The type of object or value handled by the converter.</typeparam>
  public sealed class DocumentCollectionJsonConverter<TDocument> : JsonConverter<DocumentCollection<TDocument>> where TDocument : DocumentBase
  {
    /// <summary>Reads and converts the JSON to type T.</summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The converted value.</returns>
    public override DocumentCollection<TDocument> Read(
      ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var documentCollection = new DocumentCollection<TDocument>();

      string propertyName = null;

      while (reader.Read())
      {
        if (reader.TokenType == JsonTokenType.PropertyName)
        {
          propertyName = reader.GetString();
        }
        else
        {
          if (propertyName == "_rid" && reader.TokenType == JsonTokenType.String)
          {
            documentCollection.ResourceId = reader.GetString();
          }
          else if (propertyName == "Documents" && reader.TokenType == JsonTokenType.StartArray)
          {
            //documentCollection.Documents =
            //  DocumentCollectionJsonConverter<TDocument>.ReadDocuments(
            //    ref reader, typeToConvert, options);
          }
          else if (propertyName == "_count" && reader.TokenType == JsonTokenType.Number)
          {
            documentCollection.Count = reader.GetInt32();
          }
        }
      }

      return documentCollection;
    }

    /// <summary>Writes a specified value as JSON.</summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="document">The value to convert to JSON.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(
      Utf8JsonWriter writer, DocumentCollection<TDocument> value, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    private static IEnumerable<TDocument> ReadDocuments(
      ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (options.GetConverter(typeof(IEnumerable<TDocument>)) is JsonConverter<IEnumerable<TDocument>> converter)
      {
        return converter.Read(ref reader, typeToConvert, options);
      }

      return JsonSerializer.Deserialize<IEnumerable<TDocument>>(ref reader, options);
    }
  }
}
