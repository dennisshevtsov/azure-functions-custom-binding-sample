// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  public sealed class DocumentJsonConverter<TEntity> : JsonConverter<Document<TEntity>> where TEntity : class
  {
    public static readonly IDictionary<string, PropertyInfo> DocumentProperties =
      new Dictionary<string, PropertyInfo>
      {
        { DocumentJsonConverter.IdPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Id)) },
        { DocumentJsonConverter.PartitionKeyPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.PartitionKey)) },
        { DocumentJsonConverter.RidPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Rid)) },
        { DocumentJsonConverter.SelfPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Self)) },
        { DocumentJsonConverter.EtagPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Etag)) },
        { DocumentJsonConverter.AttachmentsPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Attachments)) },
        { DocumentJsonConverter.TsPropertyName, typeof(Document<TEntity>).GetProperty(nameof(Document.Ts)) },
      };

    public static readonly IDictionary<string, PropertyInfo> EntityProperties =
      typeof(TEntity).GetProperties()
                     .ToDictionary(property => JsonNamingPolicy.CamelCase.ConvertName(property.Name),
                                   property => property);

    public override Document<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var document = new Document<TEntity>
      {
        Entity = Activator.CreateInstance<TEntity>(),
      };

      while (reader.Read())
      {
        if (reader.TokenType != JsonTokenType.PropertyName)
        {
          continue;
        }

        var propertyName = reader.GetString();

        if (!TryReadDocumentProperty(ref reader, options, document, propertyName))
        {
          ReadEntityProperty(ref reader, options, document.Entity, propertyName);
        }
      }

      return document;
    }

    public override void Write(Utf8JsonWriter writer, Document<TEntity> value, JsonSerializerOptions options)
    {
      writer.WriteStartObject();

      foreach (var documentProperty in DocumentProperties)
      {
        writer.WritePropertyName(documentProperty.Key);

        JsonSerializer.Serialize(writer, documentProperty.Value.GetValue(value), documentProperty.Value.PropertyType, options);
      }

      var entityProperties = typeof(TEntity).GetProperties();

      foreach (var entityProperty in entityProperties)
      {
        writer.WritePropertyName(options.PropertyNamingPolicy.ConvertName(entityProperty.Name));

        JsonSerializer.Serialize(writer, entityProperty.GetValue(value.Entity), entityProperty.PropertyType, options);
      }

      writer.WriteEndObject();
    }

    private bool TryReadDocumentProperty(ref Utf8JsonReader reader, JsonSerializerOptions options, Document<TEntity> document, string propertyName)
    {
      var read = false;

      if (DocumentJsonConverter<TEntity>.DocumentProperties.TryGetValue(propertyName, out var property))
      {
        reader.Read();

        var propertyValue = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);

        if (propertyValue != null)
        {
          property.SetValue(document, propertyValue);
        }

        read = true;
      }

      return read;
    }

    private void ReadEntityProperty(ref Utf8JsonReader reader, JsonSerializerOptions options, TEntity entity, string propertyName)
    {
      reader.Read();

      if (EntityProperties.TryGetValue(propertyName, out var property))
      {
        var propertyValue = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);

        property.SetValue(entity, propertyValue);
      }
    }
  }
}
