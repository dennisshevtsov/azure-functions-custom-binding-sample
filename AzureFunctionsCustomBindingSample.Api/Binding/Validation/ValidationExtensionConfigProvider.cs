// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Validation
{
  using AzureFunctionsCustomBindingSample.Api.Validators;
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the validation binding.</summary>
  public sealed class ValidationExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context)
    {
      var validatorProvider =
        new ValidatorProvider()
          .AddValidator(request =>
          {
            IValidator validator = null;

            if (request.Path != null &&
                string.Equals(request.Path.Value, "/api/product", System.StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(request.Method, "post", System.StringComparison.InvariantCultureIgnoreCase))
            {
              validator = new CreateProductValidator(request);
            }

            return validator;
          });

      context.AddBindingRule<ValidationAttribute>()
             .Bind(new ValidationBindingProvider(validatorProvider));
    }
  }
}
