// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Binds the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationValueProvider"/> with a parameter that is marked with the <see cref="Binding.ValidationAttribute"/> attribute.</summary>
  public sealed class ValidationBinding : IBinding
  {
    public const string ParameterDescriptorName = "validation";

    private readonly bool _throwIfInvalid;
    private readonly Type _validatorType;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidationBinding"/> class.</summary>
    /// <param name="throwIfInvalid">An object that indicates whether it should throw an exception if a result is invalid.</param>
    /// <param name="validatorType">An object that represents a type of a validator.</param>
    public ValidationBinding(bool throwIfInvalid, Type validatorType)
    {
      _throwIfInvalid = throwIfInvalid;
      _validatorType = validatorType ?? throw new ArgumentNullException(nameof(validatorType));
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
        var validatorFactory = ActivatorUtilities.CreateFactory(
          _validatorType,
          new[]
          {
            _validatorType.GetConstructors()[0].GetParameters()[0].ParameterType,
          });

        var validatorValueProvider = new ValidationValueProvider(
          httpRequest.HttpContext, validatorFactory, _throwIfInvalid);

        return Task.FromResult<IValueProvider>(validatorValueProvider);
      }

      throw new InvalidOperationException();
    }

    /// <summary>Gets a parameter descriptor.</summary>
    /// <returns>An object that represents a parameter descriptor.</returns>
    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = ValidationBinding.ParameterDescriptorName, };
  }
}
