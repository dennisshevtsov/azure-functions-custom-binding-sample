// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;

  /// <summary>Provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.Api.Documents.TodoListDocument"/> class.</summary>
  public sealed class TodoService : ITodoService
  {
    private readonly IDocumentClient _documentClient;

    /// <summary>Initializes a new instance of the <see cref="TodoService"/> class.</summary>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.DocumentPersistence.DocumentBase"/> class.</param>
    public TodoService(IDocumentClient documentClient)
      => _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));

    /// <summary>Creates a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to create a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CreateTodoListResponseDto> CreateTodoListAsync(
      CreateTodoListRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken)
      => _documentClient.InsertAsync(new TodoListDocument
                        {
                          Id = Guid.NewGuid(),
                          Title = requestDto.Title,
                          Description = requestDto.Description,
                          Type = nameof(TodoListDocument),
                        }, cancellationToken)
                        .ContinueWith(task => new CreateTodoListResponseDto
                        {
                          TodoListId = task.Result.Id,
                        });

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
      CancellationToken cancellationToken)
      => _documentClient.UpdateAsync(TodoService.UpdateTodoList(requestDto, todoListDocument),
                                     cancellationToken);

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
      CancellationToken cancellationToken)
    {
      var todoListTaskDocument = TodoService.AddTodoListTask(requestDto, todoListDocument);

      return _documentClient.UpdateAsync(todoListDocument, cancellationToken)
                            .ContinueWith(_ => new CreateTodoListTaskResponseDto
                            {
                              TodoListTaskId = todoListTaskDocument.TaskId,
                            });
    }

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
      CancellationToken cancellationToken)
      => TodoService.TryUpdateTodoListTask(requestDto, todoListDocument) ?
         _documentClient.UpdateAsync(todoListDocument, cancellationToken) :
         Task.CompletedTask;

    /// <summary>Completes a task of a TODO list.</summary>
    /// <param name="requestDto">An object that represents data to complete a task of a TODO list.</param>
    /// <param name="todoListDocument">An object that represents detail of a TODO list.</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task CompleteTodoListTaskAsync(
      CompleteTodoListTaskRequestDto requestDto,
      TodoListDocument todoListDocument,
      UserDocument userDocument,
      CancellationToken cancellationToken)
      => TodoService.TryCompleteTodoListTask(requestDto, todoListDocument) ?
         _documentClient.UpdateAsync(todoListDocument, cancellationToken) :
         Task.CompletedTask;

    private static TodoListDocument UpdateTodoList(
      UpdateTodoListRequestDto requestDto, TodoListDocument todoListDocument)
    {
      todoListDocument.Title = requestDto.Title;
      todoListDocument.Description = requestDto.Description;

      return todoListDocument;
    }

    private static TodoListTaskDocument AddTodoListTask(
      CreateTodoListTaskRequestDto requestDto, TodoListDocument todoListDocument)
    {
      var todoListTaskDocument = new TodoListTaskDocument
      {
        TaskId = Guid.NewGuid(),
        Title = requestDto.Title,
        Description = requestDto.Description,
        Deadline = requestDto.Deadline,
      };

      todoListDocument.Tasks = new List<TodoListTaskDocument>(
        todoListDocument.Tasks ?? Enumerable.Empty<TodoListTaskDocument>())
      {
        todoListTaskDocument
      };

      return todoListTaskDocument;
    }

    private static bool TryUpdateTodoListTask(
      UpdateTodoListTaskRequestDto requestDto, TodoListDocument todoListDocument)
    {
      var todoListTaskDocument = todoListDocument.Tasks?.FirstOrDefault(
        document => document.TaskId == requestDto.TodoListTaskId);

      if (todoListTaskDocument != null)
      {
        todoListTaskDocument.Title = requestDto.Title;
        todoListTaskDocument.Description = requestDto.Description;
        todoListTaskDocument.Deadline = requestDto.Deadline;

        return true;
      }

      return false;
    }

    private static bool TryCompleteTodoListTask(
      CompleteTodoListTaskRequestDto requestDto, TodoListDocument todoListDocument)
    {
      var todoListTaskDocument = todoListDocument.Tasks?.FirstOrDefault(
        document => document.TaskId == requestDto.TodoListTaskId);

      if (todoListTaskDocument != null)
      {
        todoListTaskDocument.Completed = true;

        return true;
      }

      return false;
    }
  }
}
