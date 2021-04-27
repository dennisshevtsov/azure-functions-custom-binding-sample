// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Documents
{
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.CosmosDb;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class TodoListDocument : DocumentBase
  {
    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<TodoListTaskDocument> Tasks { get; set; }
  }
}
