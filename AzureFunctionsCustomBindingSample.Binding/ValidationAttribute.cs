// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  /// <summary>Binds a provider that initializes a parameter with a validation result of an HTTP request.</summary>
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class ValidationAttribute : Attribute
  {
    /// <summary>Gets/sets an object that represents a type of a validator.</summary>
    public Type ValidatorType { get; set; }

    /// <summary>Gets/sets a value that indicates if it should throw an exception if a validation is faild.</summary>
    public bool ThrowIfInvalid { get; set; }
  }
}
