// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  public sealed class DocumentJsonConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type typeToConvert) => typeof(DocumentBase).IsAssignableFrom(typeToConvert);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      var converterType = typeof(DocumentJsonConverter<>).MakeGenericType(new[] { typeToConvert, });
      var converter = (JsonConverter)Activator.CreateInstance(converterType);

      return converter;
    }
  }
}
