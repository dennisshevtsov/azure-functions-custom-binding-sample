// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  /// <summary>Binds a provider that initializes a parameter with data from DB.</summary>
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class DocumentAttribute : Attribute { }
}
