// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  using System;

  /// <summary>Represents a base of a document that can be stored in Cosmos DB.</summary>
  public abstract class DocumentBase
  {
    /// <summary>Gets/sets a value that represents an ID of a document.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets/sets a value that represents a partition key of a document.</summary>
    public string Type { get; set; }

    /// <summary>Gets/sets a value that represents a rid of a document.</summary>
    public string Rid { get; set; }

    /// <summary>Gets/sets a value that represents a self of a document.</summary>
    public string Self { get; set; }

    /// <summary>Gets/sets a value that represents a etag of a document.</summary>
    public string Etag { get; set; }

    /// <summary>Gets/sets a value that represents attachments of a document.</summary>
    public string Attachments { get; set; }

    /// <summary>Gets/sets a value that represents a ts of a document.</summary>
    public int Ts { get; set; }
  }
}
