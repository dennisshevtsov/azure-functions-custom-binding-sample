// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  /// <summary>Binds a provider that initializes a parameter with data from an HTTP request.</summary>
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class RequestAttribute : Attribute { }
}
