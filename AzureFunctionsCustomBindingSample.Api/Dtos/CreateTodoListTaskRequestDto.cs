// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Binding.Document;

  /// <summary>Represents data to create a task for a TODO list.</summary>
  public sealed class CreateTodoListTaskRequestDto : ITodoListIdentity, IDocumentQuery
  {
    #region Members of ITodoListIdentity

    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    #endregion

    /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list task.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a deadline of a TODO list task.</summary>
    public DateTime Deadline { get; set; }

    #region Members of IDocumentQuery

    /// <summary>Gets/sets a value that represents an ID of a document.</summary>
    string IDocumentQuery.DocumentId => TodoListId.ToString();

    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    string IDocumentQuery.PartitionId => nameof(TodoListDocument);

    #endregion
  }
}
