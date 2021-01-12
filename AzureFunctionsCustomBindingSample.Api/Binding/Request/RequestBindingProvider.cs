// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Request
{
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  
  public sealed class RequestBindingProvider : IBindingProvider
  {
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
      => Task.FromResult<IBinding>(new RequestBinding(context.Parameter.ParameterType));
  }
}
