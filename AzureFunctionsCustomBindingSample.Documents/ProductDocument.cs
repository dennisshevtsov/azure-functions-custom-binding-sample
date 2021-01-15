// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  /// <summary>Represents detail of a product.</summary>
  public sealed class ProductDocument : DocumentBase
  {
    /// <summary>Gets/sets a value that represents a SKU of a product.</summary>
    public string Sku { get; set; }

    /// <summary>Gets/sets a value that represents a name of a product.</summary>
    public string Name { get; set; }

    /// <summary>Gets/sets a value that represents a description of a product.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a price of a product unit.</summary>
    public float PricePerUnit { get; set; }

    /// <summary>Gets/sets a value that represents a unit of a product.</summary>
    public ProductUnitDocument Unit { get; set; }

    /// <summary>Gets/sets a value that indicates if a product is enabled.</summary>
    public bool Enabled { get; set; }

    public static ProductDocument New(
      string sku,
      string name,
      string description,
      float pricePerUnit,
      UnitDocument unitDocument)
    {
      var productDocument = new ProductDocument
      {
        Sku = sku,
        Name = name,
        Description = description,
        Unit = new ProductUnitDocument
        {
          UnitId = unitDocument.Id,
          Name = unitDocument.Name,
        },
        PricePerUnit = pricePerUnit,
        Enabled = false,
      };

      return productDocument;
    }
  }
}
