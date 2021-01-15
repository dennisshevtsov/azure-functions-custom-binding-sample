// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  using System;

  /// <summary>Represents a command to create a product.</summary>
  public sealed class CreateProductRequestDto
  {
    /// <summary>Gets/sets a value that represents a name of a product.</summary>
    public string Name { get; set; }

    /// <summary>Gets/sets a value that represents a description of a product.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a price of a product unit.</summary>
    public float PricePerUnit { get; set; }

    /// <summary>Gets/sets a value that represents an ID of a unit..</summary>
    public Guid Unit { get; set; }
  }
}
