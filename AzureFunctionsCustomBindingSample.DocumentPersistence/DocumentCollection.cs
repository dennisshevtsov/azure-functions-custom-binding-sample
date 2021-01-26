
// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System.Collections.Generic;

  public sealed class DocumentCollection<TDocument> where TDocument : DocumentBase
  {
    public string _rid { get; set; }

    public IEnumerable<TDocument> Documents { get; set; }

    public int _count { get; set; }
  }
}
