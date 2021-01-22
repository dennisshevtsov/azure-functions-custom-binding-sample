// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  /// <summary>Binds a provider that initializes a parameter with a validation result of an HTTP request.</summary>
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class ValidationAttribute : Attribute
  {
    /// <summary>Gets/sets a value that indicates if it should throw an exception if a validation is faild.</summary>
    public bool ThrowIfInvalid { get; set; }
  }
}
