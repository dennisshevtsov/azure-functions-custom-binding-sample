// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System.Collections.Generic;

  /// <summary>Represents conditions to query a collection of documents.</summary>
  public interface IDocumentCollectionQuery
  {
    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    public string PartitionId { get; }

    /// <summary>Gets/sets a value that represents a query.</summary>
    public string Query { get; }

    /// <summary>Gets/sets a value that represents a collection of parameters for a query.</summary>
    public IReadOnlyDictionary<string, object> Parameters { get; }
  }
}
