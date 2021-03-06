﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.CosmosDb
{
  using System;

  /// <summary>Represents conditions to query a document.</summary>
  public interface IDocumentQuery
  {
    /// <summary>Gets/sets a value that represents an ID of a document.</summary>
    public Guid Id { get; }

    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    public string PartitionId { get; }
  }
}
