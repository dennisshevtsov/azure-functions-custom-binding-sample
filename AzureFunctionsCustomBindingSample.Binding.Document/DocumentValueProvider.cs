// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

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
    public async Task<object> GetValueAsync()
    {
      object instance = null;

      var cancellationToken = _httpRequest.HttpContext.RequestAborted;
      var documentClient = _httpRequest.HttpContext.RequestServices.GetRequiredService<IDocumentClient>();
      var requestDto = _httpRequest.HttpContext.Items["__request__"];

      if (requestDto is GetProductRequestDto getProductRequestDto)
      {
        instance = await documentClient.FirstOrDefaultAsync<ProductDocument>(
          getProductRequestDto.ProductId, nameof(ProductDocument), cancellationToken);
      }
      else if (requestDto is GetOrderRequestDto getOrderRequestDto)
      {
        instance = await documentClient.FirstOrDefaultAsync<ProductDocument>(
          getOrderRequestDto.OrderId, nameof(OrderDocument), cancellationToken);
      }
      else if (requestDto is CreateProductRequestDto createProductRequestDto)
      {
        instance = await documentClient.FirstOrDefaultAsync<UnitDocument>(
          createProductRequestDto.Unit, nameof(UnitDocument), cancellationToken);
      }
      else if (requestDto is CreateOrderRequestDto createOrderRequestDto)
      {
        var productDocumentDictionary = new Dictionary<Guid, ProductDocument>();

        await foreach (var productDocument in documentClient.AsEnumerableAsync<ProductDocument>(
          nameof(ProductDocument),
          "SELECT * FROM c WHERE ARRAY_CONTAINS(@products, c.id)",
          new Dictionary<string, object> { },
          cancellationToken))
        {
          productDocumentDictionary.Add(productDocument.Id, productDocument);
        }

        instance = productDocumentDictionary;
      }

      return instance;
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => DocumentBinding.ParameterDescriptorName;
  }
}
