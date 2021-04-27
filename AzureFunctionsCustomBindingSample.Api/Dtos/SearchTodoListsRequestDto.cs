// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Binding.Document;

  /// <summary>Represents conditions to query TODO lists.</summary>
  public sealed class SearchTodoListsRequestDto : IDocumentCollectionQuery
  {
    /// <summary>Gets/sets a value that represents a term to query TODO lists.</summary>
    public string Term { get; set; }

    /// <summary>Gets/sets a value that represents a continuation token.</summary>
    public string ContinuationToken { get; set; }

    #region Members of IDocumentCollectionQuery

    private const string TermParameterName = "@Term";

    /// <summary>Gets/sets a value that represents a partition ID of a document.</summary>
    string IDocumentCollectionQuery.PartitionId => nameof(TodoListDocument);

    /// <summary>Gets/sets a value that represents a query.</summary>
    string IDocumentCollectionQuery.Query =>
      $"SELECT * FROM c WHERE CONTAINS(c.{nameof(TodoListDocument.Title).ToLower()}, {SearchTodoListsRequestDto.TermParameterName})";

    /// <summary>Gets/sets an object that represents a collection of parameters for a query.</summary>
    IReadOnlyDictionary<string, object> IDocumentCollectionQuery.Parameters =>
      new Dictionary<string, object>
      {
        { SearchTodoListsRequestDto.TermParameterName, Term },
      };

    /// <summary>Gets/sets a value that represents a continuation token.</summary>
    string IDocumentCollectionQuery.ContinuationToken => ContinuationToken;

    #endregion
  }
}
