// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  /// <summary>Represents conditions to query orders.</summary>
  public sealed class SearchOrdersRequestDto
  {
    /// <summary>Gets/sets a value that represents an order #.</summary>
    public string OrderNo { get; set; }
  }
}
