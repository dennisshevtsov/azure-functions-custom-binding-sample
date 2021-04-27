// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Binding.Document;

  /// <summary>Represents conditions to query a task of a TODO list.</summary>
  public sealed class GetTodoListTaskRequestDto
    : ITodoListIdentity, ITodoListTaskIdentity, IDocumentQuery
  {
    #region Members of ITodoListIdentity

    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    #endregion

    #region Members of ITodoListTaskIdentity

    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }

    #endregion

    #region Members of IDocumentQuery

    /// <summary>Gets/sets a value that represents an ID of a document.</summary>
    string IDocumentQuery.DocumentId => TodoListId.ToString();

    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    string IDocumentQuery.PartitionId => nameof(TodoListDocument);

    #endregion
  }
}
