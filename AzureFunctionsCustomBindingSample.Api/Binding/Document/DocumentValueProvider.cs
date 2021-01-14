// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Document
{
  using System;
  using System.Threading.Tasks;
  
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsCustomBindingSample.Api.Binding.Request;
  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  public sealed class DocumentValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    public DocumentValueProvider(Type type, HttpRequest httpRequest)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public Type Type { get; }

    public async Task<object> GetValueAsync()
    {
      object instance = null;

      var cancellationToken = _httpRequest.HttpContext.RequestAborted;
      var documentClient = _httpRequest.HttpContext.RequestServices.GetRequiredService<IDocumentClient>();
      var requestDto = _httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName];

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

      return instance;
    }

    public string ToInvokeString() => DocumentBinding.ParameterDescriptorName;
  }
}
