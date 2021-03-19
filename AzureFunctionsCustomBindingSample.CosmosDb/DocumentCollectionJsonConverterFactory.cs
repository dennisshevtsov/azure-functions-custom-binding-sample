﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.CosmosDb
{
  using System;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  /// <summary>Supports converting several types by using a factory pattern.</summary>
  public sealed class DocumentCollectionJsonConverterFactory : JsonConverterFactory
  {
    /// <summary>Determines whether the converter instance can convert the specified object type.</summary>
    /// <param name="typeToConvert">The type of the object to check whether it can be converted by this converter instance.</param>
    /// <returns>true if the instance can convert the specified object type; otherwise, false.</returns>
    public override bool CanConvert(Type typeToConvert)
      => typeToConvert.IsGenericType &&
         typeToConvert.GetGenericTypeDefinition() == typeof(DocumentCollection<>) &&
         typeof(DocumentBase).IsAssignableFrom(typeToConvert.GetGenericArguments()[0]);

    /// <summary>Creates a converter for a specified type.</summary>
    /// <param name="typeToConvert">The type handled by the converter.</param>
    /// <param name="options">The serialization options to use.</param>
    /// <returns>A converter for which T is compatible with typeToConvert.</returns>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      var argumentType = typeToConvert.GetGenericArguments()[0];
      var converterType = typeof(DocumentCollectionJsonConverter<>).MakeGenericType(new[] { argumentType, });
      var converter = (JsonConverter)Activator.CreateInstance(converterType);

      return converter;
    }
  }
}
