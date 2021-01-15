﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Service
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class ServiceValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    public ServiceValueProvider(Type type, HttpRequest httpRequest)
    {
      Type = type ?? throw new ArgumentNullException(nameof(type));
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public Type Type { get; }

    public Task<object> GetValueAsync() => Task.FromResult(Activator.CreateInstance(Type));

    public string ToInvokeString() => ServiceBinding.ParameterDescriptorName;
  }
}