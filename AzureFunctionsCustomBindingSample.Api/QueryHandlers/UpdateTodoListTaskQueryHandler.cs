﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.QueryHandlers
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.CosmosDb;

  /// <summary>Provides a method to resolve a document with a query.</summary>
  /// <typeparam name="TQuery">A type of a query.</typeparam>
  public sealed class UpdateTodoListTaskQueryHandler : IQueryHandler<UpdateTodoListTaskRequestDto>
  {
    private readonly IDocumentClient _documentClient;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Api.QueryHandlers.UpdateTodoListTaskQueryHandler"/> class.</summary>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.CosmosDb.DocumentBase"/> class.</param>
    public UpdateTodoListTaskQueryHandler(IDocumentClient documentClient)
      => _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));

    /// <summary>Resolves a document with a query.</summary>
    /// <param name="query">An object that represents a query.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> HandleAsync(UpdateTodoListTaskRequestDto query, CancellationToken cancellationToken)
      => _documentClient.FirstOrDefaultAsync<TodoListDocument>(query.TodoListId, nameof(TodoListDocument), cancellationToken)
                        .ContinueWith(task => (object)task.Result);
  }
}
