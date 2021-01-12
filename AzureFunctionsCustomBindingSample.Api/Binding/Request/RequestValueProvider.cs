// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Request
{
  using System;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class RequestValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private BindingContext _bindingContext;

    public RequestValueProvider(Type type, HttpRequest httpRequest)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public Type Type { get; }

    public async Task<object> GetValueAsync()
    {
      var jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };
      var requestDto = await JsonSerializer.DeserializeAsync(
        _httpRequest.Body, Type, jsonSerializerOptions, _httpRequest.HttpContext.RequestAborted);

      return requestDto;
    }

    public string ToInvokeString() => RequestBinding.ParameterDescriptorName;
  }
}
