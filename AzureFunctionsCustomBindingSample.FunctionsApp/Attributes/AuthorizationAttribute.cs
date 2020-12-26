// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Attributes
{
  using System;

  using AzureFunctionsCustomBindingSample.FunctionsApp.Enums;

  public sealed class AuthorizationAttribute : Attribute
  {
    public Permission Permission { get; set; }
  }
}
