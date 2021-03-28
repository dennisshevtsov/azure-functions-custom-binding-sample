using AzureFunctionsCustomBindingSample.Api.Documents;
using AzureFunctionsCustomBindingSample.Api.Dtos;
using AzureFunctionsCustomBindingSample.Binding.Document;
using AzureFunctionsCustomBindingSample.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureFunctionsCustomBindingSample.Api.QueryHandlers
{
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
