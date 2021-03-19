// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Initializes a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.AuthorizationAttribute"/> attribute.</summary>
  public sealed class AuthorizationValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly IAuthorizedUserProvider _authorizedUserProvider;

    /// <summary>Initializes a new instance of the <see cref="AuthorizationValueProvider"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="authorizedUserProvider">An object that provides a simple API to get an authorized user.</param>
    /// <param name="type"></param>
    public AuthorizationValueProvider(HttpRequest httpRequest, IAuthorizedUserProvider authorizedUserProvider, Type type)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      _authorizedUserProvider = authorizedUserProvider ?? throw new ArgumentNullException(nameof(authorizedUserProvider));

      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type { get; }

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync()
      => _authorizedUserProvider.GetAuthorizedUserAsync(_httpRequest, _httpRequest.HttpContext.RequestAborted);

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => AuthorizationBinding.ParameterDescriptorName;
  }
}
