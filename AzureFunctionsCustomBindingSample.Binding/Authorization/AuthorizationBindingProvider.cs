// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Provides a simple API to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Authorization.AuthorizationBinding"/> class.</summary>
  public sealed class AuthorizationBindingProvider : IBindingProvider
  {
    private readonly IAuthorizedUserProvider _authorizedUserProvider;

    /// <summary>Initializes a new instance of the <see cref="AuthorizationBindingProvider"/> class.</summary>
    /// <param name="authorizedUserProvider">An object that provides a simple API to get an authorized user.</param>
    public AuthorizationBindingProvider(IAuthorizedUserProvider authorizedUserProvider)
      => _authorizedUserProvider = authorizedUserProvider ?? throw new ArgumentNullException(nameof(authorizedUserProvider));

    /// <summary>Tries to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Authorization.AuthorizationBinding"/> class.</summary>
    /// <param name="context">An object that represents detail of a binding provider context.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Authorization.AuthorizationBinding"/> class.</returns>
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
      => Task.FromResult<IBinding>(new AuthorizationBinding(_authorizedUserProvider, context.Parameter.ParameterType));
  }
}
