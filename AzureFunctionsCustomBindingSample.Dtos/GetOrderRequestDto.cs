// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  using System;

  /// <summary>Represents conditions to query an order.</summary>
  public sealed class GetOrderRequestDto
  {
    /// <summary>Gets/sets a value that represents an ID of an order.</summary>
    public Guid OrderId { get; set; }
  }
}
