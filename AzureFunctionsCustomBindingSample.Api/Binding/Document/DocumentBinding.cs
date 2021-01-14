// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Document
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class DocumentBinding : IBinding
  {
    public const string ParameterDescriptorName = "entity";

    private readonly Type _parameterType;

    public DocumentBinding(Type parameterType)
      => _parameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => throw new InvalidOperationException();

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new DocumentValueProvider(_parameterType, httpRequest));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = DocumentBinding.ParameterDescriptorName, };
  }
}
