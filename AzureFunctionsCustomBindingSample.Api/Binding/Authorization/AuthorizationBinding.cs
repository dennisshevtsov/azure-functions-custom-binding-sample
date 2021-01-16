﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Authorization
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  /// <summary>Binds the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.Authorization.AuthorizationValueProvider"/> with a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.AuthorizeAttribute"/> attribute.</summary>
  public sealed class AuthorizationBinding : IBinding
  {
    public const string ParameterDescriptorName = "authorization";

    /// <summary>Gets a value that indicates if a binding from an attribute.</summary>
    public bool FromAttribute => true;

    /// <summary>Gets a value provider to initialize a bind parameter.</summary>
    /// <param name="value">An object that represents a bind paramter value.</param>
    /// <param name="context">An object that represents a binding context.</param>
    /// <returns>A value that represents an async operation.</returns>
    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => throw new InvalidOperationException();

    /// <summary>Gets a value provider to initialize a bind parameter.</summary>
    /// <param name="context">An object that represents a binding context.</param>
    /// <returns>A value that represents an async operation.</returns>
    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new AuthorizationValueProvider(httpRequest));
      }

      throw new InvalidOperationException();
    }

    /// <summary>Gets a parameter descriptor.</summary>
    /// <returns>An object that represents a parameter descriptor.</returns>
    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = AuthorizationBinding.ParameterDescriptorName, };
  }
}
