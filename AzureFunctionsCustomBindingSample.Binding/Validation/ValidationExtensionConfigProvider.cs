// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the validation binding.</summary>
  public sealed class ValidationExtensionConfigProvider : IExtensionConfigProvider
  {
    /// <summary>Registers the validaiton binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context)
      => context.AddBindingRule<ValidationAttribute>()
                .Bind(new ValidationBindingProvider());
  }
}
