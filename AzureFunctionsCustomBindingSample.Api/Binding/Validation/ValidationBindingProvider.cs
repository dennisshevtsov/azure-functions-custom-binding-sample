// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Validation
{
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Provides a simple API to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.Validation.ValidationBinding"/> class.</summary>
  public sealed class ValidationBindingProvider : IBindingProvider
  {
    /// <summary>Tries to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.Validation.ValidationBinding"/> class.</summary>
    /// <param name="context">An object that represents detail of a binding provider context.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Binding.Validation.ValidationBinding"/> class.</returns>
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
      => Task.FromResult<IBinding>(new ValidationBinding(context.Parameter.ParameterType));
  }
}
