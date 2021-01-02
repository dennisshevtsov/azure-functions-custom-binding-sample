// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  /// <summary>Represents options of a request to Cosmos DB.</summary>
  public sealed class RequestOptions
  {
    /// <summary>Gets/sets a value that represents how many items there can be in a request.</summary>
    public int ItemsPerRequest { get; set; }
  }
}
