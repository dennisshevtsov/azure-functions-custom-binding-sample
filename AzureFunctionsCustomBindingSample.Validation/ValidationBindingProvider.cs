// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Provides a simple API to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.Validation.ValidationBinding"/> class.</summary>
  public sealed class ValidationBindingProvider : IBindingProvider
  {
    private readonly IValidatorProvider _validatorProvider;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.ValidationBindingProvider"/> class.</summary>
    /// <param name="validatorProvider">An object that provides a simple API to get an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</param>
    public ValidationBindingProvider(IValidatorProvider validatorProvider)
      => _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));

    /// <summary>Tries to create an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.Validation.ValidationBinding"/> class.</summary>
    /// <param name="context">An object that represents detail of a binding provider context.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.Validation.ValidationBinding"/> class.</returns>
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
      => Task.FromResult<IBinding>(new ValidationBinding(_validatorProvider));
  }
}
