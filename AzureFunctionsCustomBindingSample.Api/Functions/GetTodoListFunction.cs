// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.Binding.Request;
  using AzureFunctionsCustomBindingSample.Binding.Authorization;
  using AzureFunctionsCustomBindingSample.Documents;

  public static class GetTodoListFunction
  {
    /// <summary>Gets a TODO list.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents conditions to query a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents an authorized user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    [FunctionName(nameof(GetTodoListFunction))]
    public static TodoListDocument ExecuteAsync(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest httpRequest,
      [Request] GetTodoListRequestDto requestDto,
      [Document] TodoListDocument todoListDocument,
      [Authorization] UserDocument userDocument,
      CancellationToken cancellationToken) => todoListDocument;
  }
}
