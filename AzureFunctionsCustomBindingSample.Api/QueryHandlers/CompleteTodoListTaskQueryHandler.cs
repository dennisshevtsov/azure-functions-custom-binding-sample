// Copyright (c) Dennis Shevtsov. All rights reserved.
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

  public sealed class CompleteTodoListTaskQueryHandler : IQueryHandler<CompleteTodoListTaskRequestDto>
  {
    private readonly IDocumentClient _documentClient;

    public CompleteTodoListTaskQueryHandler(IDocumentClient documentClient)
      => _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));

    public Task<object> HandleAsync(CompleteTodoListTaskRequestDto query, CancellationToken cancellationToken)
      => _documentClient.FirstOrDefaultAsync<TodoListDocument>(query.TodoListId, nameof(TodoListDocument), cancellationToken)
                        .ContinueWith(task => (object) task.Result);
  }
}
