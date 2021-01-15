// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  using System;
  using System.Collections.Generic;

  /// <summary>Represents a command to create an order.</summary>
  public sealed class CreateOrderRequestDto
  {
    /// <summary>Gets/sets an object that represents a collection of ordered products.</summary>
    public IDictionary<Guid, int> Products { get; set; }
  }
}
