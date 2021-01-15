// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  /// <summary>Represents conditions to query products.</summary>
  public sealed class SearchProductsRequestDto
  {
    /// <summary>Gets/sets a value that represents a part of a product name.</summary>
    public string Name { get; set; }
  }
}
