// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the authorization binding.</summary>
  public sealed class AuthorizationExtensionConfigProvider : IExtensionConfigProvider
  {
    /// <summary>Registers the authorization binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<AuthorizationAttribute>()
                                                                     .Bind(new AuthorizationBindingProvider());
  }
}
