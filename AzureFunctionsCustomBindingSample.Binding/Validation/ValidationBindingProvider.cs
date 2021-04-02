// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Reflection;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Provides a simple API to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.Validation.ValidationBinding"/> class.</summary>
  public sealed class ValidationBindingProvider : IBindingProvider
  {
    /// <summary>Tries to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.Validation.ValidationBinding"/> class.</summary>
    /// <param name="context">An object that represents detail of a binding provider context.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.Validation.ValidationBinding"/> class.</returns>
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
    {
      var attribute = context.Parameter.GetCustomAttribute<ValidationAttribute>();

      if (attribute.ValidatorType == default ||
          !typeof(IValidator).IsAssignableFrom(attribute.ValidatorType))
      {
        throw new InvalidOperationException($"Parameter {nameof(ValidationAttribute.ValidatorType)} should be assigned with a type that inherits interface {nameof(IValidator)}.");
      }

      IBinding binding = new ValidationBinding(attribute.ThrowIfInvalid, attribute.ValidatorType);

      return Task.FromResult(binding);
    }
  }
}
