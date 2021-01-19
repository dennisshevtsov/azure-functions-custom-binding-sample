// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the validation binding.</summary>
  public sealed class ValidationExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context)
    {
      var validatorProvider =
        new ValidatorProvider();
          //.AddValidator<CreateProductValidator>("/api/product", "post");

      context.AddBindingRule<ValidationAttribute>()
             .Bind(new ValidationBindingProvider(validatorProvider));
    }
  }
}
