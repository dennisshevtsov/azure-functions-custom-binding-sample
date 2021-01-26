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
  public sealed class DocumentCollectionJsonConverter<TDocument> : JsonConverter<IEnumerable<TDocument>> where TDocument : DocumentBase
  {
    /// <summary>Reads and converts the JSON to type T.</summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>The converted value.</returns>
    public override IEnumerable<TDocument> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    /// <summary>Writes a specified value as JSON.</summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="document">The value to convert to JSON.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, IEnumerable<TDocument> value, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }
  }
}
