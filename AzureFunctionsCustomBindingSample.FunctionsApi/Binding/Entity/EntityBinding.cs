// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Entity
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class EntityBinding : IBinding
  {
    public const string ParameterDescriptorName = "entity";

    private readonly Type _parameterType;

    public EntityBinding(Type parameterType)
      => _parameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => throw new InvalidOperationException();

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new EntityValueProvider(_parameterType, httpRequest));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = EntityBinding.ParameterDescriptorName, };
  }
}
