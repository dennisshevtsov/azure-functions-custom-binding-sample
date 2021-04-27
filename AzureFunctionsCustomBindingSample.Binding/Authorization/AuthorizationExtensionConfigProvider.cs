// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System;
  using AzureFunctionsCustomBindingSample.Binding;
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the authorization binding.</summary>
  public sealed class AuthorizationExtensionConfigProvider : IExtensionConfigProvider
  {
    private readonly IAuthorizedUserProvider _authorizedUserProvider;

    /// <summary>Initializes a new instance of the <see cref="AuthorizationExtensionConfigProvider"/> class.</summary>
    /// <param name="authorizedUserProvider">An object that provides a simple API to get an authorized user.</param>
    public AuthorizationExtensionConfigProvider(IAuthorizedUserProvider authorizedUserProvider)
      => _authorizedUserProvider = authorizedUserProvider ?? throw new ArgumentNullException(nameof(authorizedUserProvider));

    /// <summary>Registers the authorization binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context)
      => context.AddBindingRule<AuthorizationAttribute>()
                .Bind(new AuthorizationBindingProvider(_authorizedUserProvider));
  }
}
