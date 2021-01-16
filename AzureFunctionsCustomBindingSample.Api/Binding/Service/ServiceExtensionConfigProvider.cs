// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Service
{
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the service binding.</summary>
  public sealed class ServiceExtensionConfigProvider : IExtensionConfigProvider
  {
    /// <summary>Registers the service binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<ServiceAttribute>()
                                                                     .Bind(new ServiceBindingProvider());
  }
}
