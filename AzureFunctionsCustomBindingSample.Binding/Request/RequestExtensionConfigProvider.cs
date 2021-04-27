// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request
{
  using AzureFunctionsCustomBindingSample.Binding;
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the request binding.</summary>
  public sealed class RequestExtensionConfigProvider : IExtensionConfigProvider
  {
    /// <summary>Registers the request binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<RequestAttribute>()
                                                                     .Bind(new RequestBindingProvider());
  }
}
