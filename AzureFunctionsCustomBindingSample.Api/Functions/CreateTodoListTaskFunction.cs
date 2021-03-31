// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Services;
  using AzureFunctionsCustomBindingSample.Api.Validators;
  using AzureFunctionsCustomBindingSample.Binding;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public static class CreateTodoListTaskFunction
  {
    /// <summary>Creates a task for a TODO list.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents data to create a task for a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents an authorized user.</param>
    /// <param name="validationResult">An object that represents detail of an validation result.</param>
    /// <param name="service">An object that provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    [FunctionName(nameof(CreateTodoListTaskFunction))]
    public static async Task<CreateTodoListTaskResponseDto> ExecuteAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task")] HttpRequest httpRequest,
      [Request] CreateTodoListTaskRequestDto requestDto,
      [Document] TodoListDocument todoListDocument,
      [Authorization] UserDocument userDocument,
      [Validation(ValidatorType = typeof(CreateTodoListTaskValidator),
                  ThrowIfInvalid = true)] ValidationResult validationResult,
      [Service] ITodoService service,
      CancellationToken cancellationToken)
      => await service.CreateTodoListTaskAsync(requestDto, todoListDocument, userDocument, cancellationToken);
  }
}
