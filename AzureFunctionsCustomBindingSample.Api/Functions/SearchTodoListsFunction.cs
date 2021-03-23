
namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Collections.Generic;
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public static class SearchTodoListsFunction
  {
    /// <summary>Gets a collection of instances of the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class that satisfies conditions that the <see cref="AzureFunctionsCustomBindingSample.Api.Dtos.SearchTodoListsRequestDto"/> class represents.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents conditions to query products.</param>
    /// <param name="documents">An object that exposes the enumerator, which supports a simple iteration over a collection of a specified type. The type is the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="userDocument">An object that represents an authorized user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that exposes the enumerator, which supports a simple iteration over a collection of a specified type. The type is the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class.</returns>
    [FunctionName(nameof(SearchTodoListsFunction))]
    public static IEnumerable<TodoListDocument> ExecuteAsync(
      [HttpTrigger("get", Route = "todo")] HttpRequest httpRequest,
      [Request] SearchTodoListsRequestDto requestDto,
      [Document] IEnumerable<TodoListDocument> documents,
      [Authorization] UserDocument userDocument,
      CancellationToken cancellationToken) => documents;
  }
}
