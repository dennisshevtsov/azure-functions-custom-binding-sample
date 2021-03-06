﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  /// <summary>Represents a set of permissions.</summary>
  [Flags]
  public enum Permission : byte
  {
    /// <summary>A value that indicates that a user has no specific permission.</summary>
    None = 0,

    /// <summary>A value that indicates that a user is regular.</summary>
    User = 1,

    /// <summary>A value that indicates that a user is an administrator.</summary>
    Administrator = 2,
  }
}
