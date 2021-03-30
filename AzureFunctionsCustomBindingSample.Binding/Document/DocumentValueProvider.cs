// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

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
      var query = _httpRequest.HttpContext.Items["__request__"];

      if (query != null)
      {
        var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), Type);
        var queryHandler = _httpRequest.HttpContext.RequestServices.GetService(queryHandlerType);

        if (queryHandler != null)
        {
          var handleMethod = queryHandlerType.GetMethod(nameof(IQueryHandler<object, object>.HandleAsync));

          if (handleMethod != null)
          {
            var cancellationToken = _httpRequest.HttpContext.RequestAborted;
            var task = (Task)handleMethod.Invoke(queryHandler, new object[] { query, cancellationToken, });
            var taskWithResult = task.ContinueWith(
              (task, result) => typeof(Task<>).MakeGenericType(Type)
                                              .GetProperty(nameof(Task<object>.Result))
                                              .GetValue(task),
              cancellationToken);

            return taskWithResult;
          }
        }
      }

      return Task.FromResult(default(object));
    }

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => DocumentBinding.ParameterDescriptorName;
  }
}
