// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Documents;

  /// <summary>Provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class.</summary>
  public interface ITodoService
  {
    /// <summary>Creates a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to create a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CreateTodoListResponseDto> CreateTodoListAsync(
      CreateTodoListRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>Updates a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to update a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto requestDto,
      TodoListDocument todoListDocument,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>Creates a task of a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to create a task for a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(
      CreateTodoListTaskRequestDto requestDto,
      TodoListDocument todoListDocument,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>Updates a task of a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to a task of a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task UpdateTodoListTaskAsync(
      UpdateTodoListTaskRequestDto requestDto,
      TodoListDocument todoListDocument,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>Completes a task of a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to complete a task of a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CompleteTodoListTaskResponseDto> CompleteTodoListTaskAsync(
      CompleteTodoListTaskRequestDto requestDto,
      TodoListDocument todoListDocument,
      UserDocument userDocument,
      CancellationToken cancellationToken);
  }
}
