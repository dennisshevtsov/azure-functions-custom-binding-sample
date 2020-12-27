// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Binding
{
  using System;

  using Microsoft.Azure.WebJobs.Description;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Enums;

  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class AuthorizationAttribute : Attribute
  {
    public Permission Permission { get; set; }
  }
}
