// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System;

  /// <summary>Represents an error that occurs if an authorized user tries access a secured resource.</summary>
  public sealed class UnauthorizedException : Exception
  {
    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Authorization.UnauthorizedException"/> class.</summary>
    public UnauthorizedException()
      : base("Unauthorized.")
    {
    }
  }
}
