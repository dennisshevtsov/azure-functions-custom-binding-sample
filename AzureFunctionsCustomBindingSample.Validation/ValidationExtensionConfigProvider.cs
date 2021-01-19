// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System;

  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the validation binding.</summary>
  public sealed class ValidationExtensionConfigProvider : IExtensionConfigProvider
  {
    private readonly IValidatorProvider _validatorProvider;

    public ValidationExtensionConfigProvider(IValidatorProvider validatorProvider)
      => _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));

    /// <summary>Registers the validaiton binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context)
      => context.AddBindingRule<ValidationAttribute>()
                .Bind(new ValidationBindingProvider(_validatorProvider));
  }
}
