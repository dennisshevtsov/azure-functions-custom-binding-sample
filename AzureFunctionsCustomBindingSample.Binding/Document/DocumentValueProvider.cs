// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.DocumentAttribute"/> attribute.</summary>
  public sealed class DocumentValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    /// <summary>Initializes a new instance of the <see cref="DocumentValueProvider"/> class.</summary>
    /// <param name="type">A value that represents a parameter type.</param>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public DocumentValueProvider(Type type, HttpRequest httpRequest)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type { get; }

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
    {
      var documentProvider = _httpRequest.HttpContext.RequestServices.GetRequiredService<IDocumentProvider>();
      var collectionElementType = DocumentValueProvider.GetCollectionElementType(Type);

      if (collectionElementType != null)
      {
        return documentProvider.GetDocumentsAsync(_httpRequest, collectionElementType, _httpRequest.HttpContext.RequestAborted);
      }

      return documentProvider.GetDocumentAsync(_httpRequest, Type, _httpRequest.HttpContext.RequestAborted);
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => DocumentBinding.ParameterDescriptorName;

    private static Type GetCollectionElementType(Type parameterType)
    {
      Type collectionElementType = null;

      if (parameterType.IsGenericType)
      {
        var genericType = parameterType.GetGenericTypeDefinition();

        if (typeof(IEnumerable<>).IsAssignableFrom(genericType))
        {
          collectionElementType = parameterType.GetGenericArguments()[0];
        }
      }

      return collectionElementType;
    }
  }
}
