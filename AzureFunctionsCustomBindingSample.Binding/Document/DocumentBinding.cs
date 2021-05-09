// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  /// <summary>Binds the <see cref="AzureFunctionsCustomBindingSample.Binding.Document.DocumentValueProvider"/> with a parameter that is marked with the <see cref="AzureFunctionsCustomBindingSample.Binding.DocumentAttribute"/> attribute.</summary>
  public sealed class DocumentBinding : IBinding
  {
    public const string ParameterDescriptorName = "entity";

    private readonly Type _parameterType;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Document.DocumentBinding"/> class.</summary>
    /// <param name="parameterType">Gets a value that represents a type of a binded parameter.</param>
    public DocumentBinding(Type parameterType)
      => _parameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));

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
        return Task.FromResult<IValueProvider>(new DocumentValueProvider(
          _parameterType, httpRequest, context.FunctionCancellationToken));
      }

      throw new InvalidOperationException();
    }

    /// <summary>Gets a parameter descriptor.</summary>
    /// <returns>An object that represents a parameter descriptor.</returns>
    public ParameterDescriptor ToParameterDescriptor()
      => new ParameterDescriptor { Name = DocumentBinding.ParameterDescriptorName, };
  }
}
