// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  /// <summary>Represents conditions to query TODO lists.</summary>
  public sealed class SearchTodoListsRequestDto
  {
    /// <summary>Gets/sets a value that represents a term to query TODO lists.</summary>
    public string Term { get; set; }
  }
}
