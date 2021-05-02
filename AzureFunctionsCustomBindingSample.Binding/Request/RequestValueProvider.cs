// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.Primitives;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.RequestAttribute"/> attribute.</summary>
  public sealed class RequestValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    /// <summary>Initializes a new instance of the <see cref="RequestValueProvider"/> class.</summary>
    /// <param name="type">A value that represents a parameter type.</param>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public RequestValueProvider(Type type, HttpRequest httpRequest)
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

      if (_httpRequest.Body == null || !_httpRequest.Body.CanSeek || _httpRequest.Body.Length == 0)
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

      Populate(instance, _httpRequest.HttpContext.GetRouteData().Values);
      Populate(instance, _httpRequest.Query);

      _httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] = instance;

      return instance;
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => RequestBinding.ParameterDescriptorName;

    private void Populate<TValue>(
      object instance,
      IEnumerable<KeyValuePair<string, TValue>> parameters)
    {
      foreach (var queryParameter in parameters)
      {
        var property = Type.GetProperty(queryParameter.Key, BindingFlags.Public |
                                                            BindingFlags.SetProperty |
                                                            BindingFlags.IgnoreCase |
                                                            BindingFlags.Instance);

        if (property != null)
        {
          if (property.PropertyType == typeof(Guid))
          {
            if (Guid.TryParse(queryParameter.Value.ToString(), out var value))
            {
              property.SetValue(instance, value);
            }
          }
          else if (property.PropertyType == typeof(int))
          {
            if (int.TryParse(queryParameter.Value.ToString(), out var value))
            {
              property.SetValue(instance, value);
            }
          }
          else if (property.PropertyType == typeof(string))
          {
            property.SetValue(instance, queryParameter.Value.ToString());
          }
        }
      }
    }
  }
}
