// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.EntityModel
{
  using System;

  /// <summary>Represents details of a product.</summary>
  public sealed class ProductEntity
  {
    /// <summary>Gets/sets a value that represents a name of a product.</summary>
    public string Name { get; set; }

    /// <summary>Gets/sets a value that represents a description of a product.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a price of a product.</summary>
    public float Price { get; set; }

    public DateTime CreateOn { get; set; }

    public static ProductEntity New(string name, string description, float price, DateTime createdOn)
      => new ProductEntity
      {
        Name = name,
        Description = description,
        Price = price,
        CreateOn = createdOn,
      };
  }
}
