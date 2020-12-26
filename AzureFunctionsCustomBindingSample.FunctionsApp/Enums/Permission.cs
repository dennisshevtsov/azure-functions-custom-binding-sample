// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Enums
{
  using System;

  [Flags]
  public enum Permission : byte
  {
    None = 0,
    User = 1,
    Administrator = 2,
  }
}
