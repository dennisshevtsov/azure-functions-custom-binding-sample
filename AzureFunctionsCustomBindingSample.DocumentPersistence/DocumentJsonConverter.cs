// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.
namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  public sealed class DocumentJsonConverter<TDocument> : JsonConverter<TDocument> where TDocument : DocumentBase
  {
    public override TDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var document = Activator.CreateInstance<TDocument>();

      while (reader.Read())
      {
        if (reader.TokenType != JsonTokenType.PropertyName)
        {
          continue;
        }

        var jsonPropertyName = reader.GetString();
        var property = GetProperty(jsonPropertyName, options);
        var propertyValue = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);

        property.SetValue(document, propertyValue);
      }

      return document;
    }

    public override void Write(Utf8JsonWriter writer, TDocument document, JsonSerializerOptions options)
    {
      writer.WriteStartObject();

      var properties = typeof(TDocument).GetProperties();

      foreach (var property in properties)
      {
        writer.WritePropertyName(GetJsonPropertyName(property, options));

        JsonSerializer.Serialize(writer, property.GetValue(document), property.PropertyType, options);
      }

      writer.WriteEndObject();
    }

    private IDictionary<string, PropertyInfo> _properties;
    private IDictionary<string, PropertyInfo> GetProperties(JsonSerializerOptions options)
    {
      if (_properties == null)
      {
        _properties = new Dictionary<string, PropertyInfo>();

        foreach (var property in typeof(TDocument).GetProperties())
        {
          _properties.Add(GetJsonPropertyName(property, options), property);
        }
      }

      return _properties;
    }

    private string GetJsonPropertyName(PropertyInfo property, JsonSerializerOptions options)
    {
      string jsonPropertyName;

      if (!DocumentPropertyNames.TypeJsonNameDictionary.TryGetValue(property.Name, out jsonPropertyName))
      {
        jsonPropertyName = options.PropertyNamingPolicy.ConvertName(property.Name);
      }

      return jsonPropertyName;
    }

    private PropertyInfo GetProperty(string jsonPropertyName, JsonSerializerOptions options)
      => GetProperties(options)[jsonPropertyName];
  }
}
