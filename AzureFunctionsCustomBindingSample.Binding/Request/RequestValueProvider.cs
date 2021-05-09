// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.RequestAttribute"/> attribute.</summary>
  public sealed class RequestValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly CancellationToken _cancellationToken;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Request.RequestValueProvider"/> class.</summary>
    /// <param name="type">A value that represents a parameter type.</param>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    public RequestValueProvider(Type type, HttpRequest httpRequest, CancellationToken cancellationToken)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      _cancellationToken = cancellationToken;
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type { get; }

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public async Task<object> GetValueAsync()
    {
      var instance = await ReadBodyAsync(_cancellationToken);

      PopulateParameters(instance, _httpRequest.HttpContext.GetRouteData().Values);
      PopulateParameters(instance, _httpRequest.Query);

      _httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] = instance;

      return instance;
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => RequestBinding.ParameterDescriptorName;

    private ValueTask<object> ReadBodyAsync(CancellationToken cancellationToken)
    {
      if (_httpRequest.Body == null || !_httpRequest.Body.CanSeek || _httpRequest.Body.Length == 0)
      {
        return new ValueTask<object>(Task.FromResult(Activator.CreateInstance(Type)));
      }

      var jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      return JsonSerializer.DeserializeAsync(
        _httpRequest.Body, Type, jsonSerializerOptions, cancellationToken);
    }

    private void PopulateParameters<TValue>(
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
