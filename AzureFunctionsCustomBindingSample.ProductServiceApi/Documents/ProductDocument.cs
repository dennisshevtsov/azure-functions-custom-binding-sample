// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.ProductServiceApi.Documents
{
  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  public sealed class ProductDocument : DocumentBase
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public float PricePerUnit { get; set; }

    public string Unit { get; set; }

    public bool Enabled { get; set; }
  }
}
