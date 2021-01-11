// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  
  public sealed class OrderDocument : DocumentBase
  {
    public float TotalPrice { get; set; }

    public IEnumerable<OrderProductDocument> Products { get; set; }
  }
}
