// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request
{
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Provides a simple API to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Request.RequestBinding"/> class.</summary>
  public sealed class RequestBindingProvider : IBindingProvider
  {
    /// <summary>Tries to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Request.RequestBinding"/> class.</summary>
    /// <param name="context">An object that represents detail of a binding provider context.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Request.RequestBinding"/> class.</returns>
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
      => Task.FromResult<IBinding>(new RequestBinding(context.Parameter.ParameterType));
  }
}
