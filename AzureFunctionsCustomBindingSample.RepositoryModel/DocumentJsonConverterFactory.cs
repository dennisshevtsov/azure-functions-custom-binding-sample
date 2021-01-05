using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  public sealed class DocumentJsonConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type typeToConvert)
      => typeToConvert.IsGenericType &&
         typeToConvert.GetGenericTypeDefinition() == typeof(Document<>);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      var entityType = typeToConvert.GetGenericArguments()[0];
      var converterType = typeof(DocumentJsonConverter<>).MakeGenericType(new[] { entityType, });

      var converter = (JsonConverter)Activator.CreateInstance(converterType);

      return converter;
    }
  }
}
