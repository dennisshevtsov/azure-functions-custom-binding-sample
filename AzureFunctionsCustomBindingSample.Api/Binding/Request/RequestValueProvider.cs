// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Request
{
  using System;
  using System.Reflection;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class RequestValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    public RequestValueProvider(Type type, HttpRequest httpRequest)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public Type Type { get; }

    public async Task<object> GetValueAsync()
    {
      object instance = null;

      if (_httpRequest.Body == null || _httpRequest.Body.Length == 0)
      {
        instance = Activator.CreateInstance(Type);
      }
      else
      {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        instance = await JsonSerializer.DeserializeAsync(
          _httpRequest.Body, Type, jsonSerializerOptions, _httpRequest.HttpContext.RequestAborted);
      }

      foreach (var route in _httpRequest.HttpContext.GetRouteData().Values)
      {
        var property = Type.GetProperty(route.Key, BindingFlags.Public |
                                                   BindingFlags.SetProperty |
                                                   BindingFlags.IgnoreCase |
                                                   BindingFlags.Instance);

        if (property != null)
        {
          if (property.PropertyType == typeof(Guid))
          {
            if (Guid.TryParse(route.Value.ToString(), out var value))
            {
              property.SetValue(instance, value);
            }
          }
          else if (property.PropertyType == typeof(int))
          {
            if (int.TryParse(route.Value.ToString(), out var value))
            {
              property.SetValue(instance, value);
            }
          }
        }
      }

      _httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] = instance;

      return instance;
    }

    public string ToInvokeString() => RequestBinding.ParameterDescriptorName;
  }
}
