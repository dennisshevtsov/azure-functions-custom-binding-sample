// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  /// <summary>Binds the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationValueProvider"/> with a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationBinding : IBinding
  {
    public const string ParameterDescriptorName = "validation";

    private readonly IValidatorProvider _validatorProvider;
    private readonly bool _throwIfInvalid;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationBinding"/> class.</summary>
    /// <param name="validatorProvider">An object that provides a simple API to get an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.IValidator"/> type that associated with a request.</param>
    /// <param name="throwIfInvalid">An object that indicates whether it should throw an exception if a result is invalid.</param>
    public ValidationBinding(
      IValidatorProvider validatorProvider,
      bool throwIfInvalid)
    {
      _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));
      _throwIfInvalid = throwIfInvalid;
    }

    /// <summary>Gets a value that indicates if a binding from an attribute.</summary>
    public bool FromAttribute => true;

    /// <summary>Gets a value provider to initialize a bind parameter.</summary>
    /// <param name="value">An object that represents a bind paramter value.</param>
    /// <param name="context">An object that represents a binding context.</param>
    /// <returns>A value that represents an async operation.</returns>
    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => throw new InvalidOperationException();

    /// <summary>Gets a value provider to initialize a bind parameter.</summary>
    /// <param name="context">An object that represents a binding context.</param>
    /// <returns>A value that represents an async operation.</returns>
    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(
          new ValidationValueProvider(_validatorProvider, httpRequest, _throwIfInvalid));
      }

      throw new InvalidOperationException();
    }

    /// <summary>Gets a parameter descriptor.</summary>
    /// <returns>An object that represents a parameter descriptor.</returns>
    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = ValidationBinding.ParameterDescriptorName, };
  }
}
