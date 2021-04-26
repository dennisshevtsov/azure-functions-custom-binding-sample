// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  /// <summary>Binds a provider that initializes a parameter with an authorized user.</summary>
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class AuthorizationAttribute : Attribute
  {
    /// <summary>Gets/sets a value that represents required permission.</summary>
    public Permission Permission { get; set; }
  }
}
