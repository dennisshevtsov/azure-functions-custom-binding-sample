// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  using System;

  public abstract class Document
  {
      public string Id { get; set; }

      public string PartitionKey { get; set; }

      public string Rid { get; set; }

      public string Self { get; set; }

      public Guid Etag { get; set; }

      public string Attachments { get; set; }

      public int Ts { get; set; }
  }
}
