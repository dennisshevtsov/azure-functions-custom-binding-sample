// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.
namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.CosmosDb;

  /// <summary>Represents conditions to query a TODO list.</summary>
  public sealed class GetTodoListRequestDto : ITodoListIdentity, IDocumentQuery
  {
    #region Members of ITodoListIdentity

    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    #endregion

    #region Members of IDocumentQuery

    /// <summary>Gets/sets a value that represents an ID of a document.</summary>
    Guid IDocumentQuery.Id => TodoListId;

    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    string IDocumentQuery.PartitionId => nameof(TodoListDocument);

    #endregion
  }
}
