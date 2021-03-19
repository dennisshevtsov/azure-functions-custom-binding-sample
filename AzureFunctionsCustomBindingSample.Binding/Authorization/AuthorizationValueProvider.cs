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

    /// <summary>Initializes a new instance of the <see cref="AuthorizationValueProvider"/> class.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    public AuthorizationValueProvider(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    /// <summary>Gets a value that represents a type of a parameter.</summary>
    public Type Type => typeof(UserDocument);

    /// <summary>Gets an instance of a parameter.</summary>
    /// <returns>An instance of a parameter.</returns>
    public Task<object> GetValueAsync() => Task.FromResult<object>(new UserDocument
    {
      Id = new Guid("3430ca38-0a71-46b3-8da2-5e3e866bad38"),
      Name = "Test Test",
      Address = new UserAddressDocument
      {
        AddressLine = "test st. 123-12",
        City = "Test",
        Zip = "12345",
      },
      Phone = "123-12-12",
      Email = "test@test.test",
    });

    /// <summary>Gets an invoke string.</summary>
    /// <returns>An invoke string.</returns>
    public string ToInvokeString() => AuthorizationBinding.ParameterDescriptorName;
  }
}
