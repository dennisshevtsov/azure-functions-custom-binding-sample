// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  /// <summary>Binds the <see cref="AzureFunctionsCustomBindingSample.Binding.Request.RequestValueProvider"/> with a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.RequestAttribute"/> attribute.</summary>
  public sealed class RequestBinding : IBinding
  {
    public const string ParameterDescriptorName = "__request__";

    private readonly Type _parameterType;

    /// <summary>Initializes a new instance of the <see cref="RequestBinding"/> class.</summary>
    /// <param name="parameterType">Gets a value that represents a type of a binded parameter.</param>
    public RequestBinding(Type parameterType)
      => _parameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));

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
        return Task.FromResult<IValueProvider>(new RequestValueProvider(_parameterType, httpRequest));
      }

      throw new InvalidOperationException();
    }

    /// <summary>Gets a parameter descriptor.</summary>
    /// <returns>An object that represents a parameter descriptor.</returns>
    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = RequestBinding.ParameterDescriptorName, };
  }
}
