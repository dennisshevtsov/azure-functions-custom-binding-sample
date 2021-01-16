﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Authorization
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  using AzureFunctionsCustomBindingSample.Documents;

  public sealed class AuthorizationValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    public AuthorizationValueProvider(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    public Type Type => typeof(UserDocument);

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

    public string ToInvokeString() => AuthorizationBinding.ParameterDescriptorName;
  }
}
