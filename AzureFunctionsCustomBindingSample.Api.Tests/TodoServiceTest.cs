// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Tests
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Services;
  using AzureFunctionsCustomBindingSample.CosmosDb;

  [TestClass]
  public sealed class TodoServiceTest
  {
    private Mock<IDocumentClient> _documentClientMock;
    private ITodoService _todoService;

    [TestInitialize]
    public void Initialize()
    {
      _documentClientMock = new Mock<IDocumentClient>();
      _todoService = new TodoService(_documentClientMock.Object);
    }

    [TestMethod]
    public async Task CreateTodoListAsync()
    {
      var requestDto = new CreateTodoListRequestDto
      {
        Title = TodoServiceTest.GetRandomToken(),
        Description = TodoServiceTest.GetRandomToken(),
      };
      var userDocument = new UserDocument();

      _documentClientMock.Setup(client => client.InsertAsync(
                           It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new TodoListDocument
                         {
                           Id = Guid.NewGuid(),
                         });

      var responseDto = await _todoService.CreateTodoListAsync(
        requestDto, userDocument, CancellationToken.None);

      Assert.IsNotNull(responseDto);
      Assert.IsFalse(responseDto.TodoListId == default, "TodoListId == default");

      _documentClientMock.Verify();
    }

    [TestMethod]
    public async Task UpdateTodoListAsync()
    {
      var requestDto = new UpdateTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = TodoServiceTest.GetRandomToken(),
        Description = TodoServiceTest.GetRandomToken(),
      };
      var todoListDocument = new TodoListDocument();
      var userDocument = new UserDocument();

      _documentClientMock.Setup(client => client.UpdateAsync(
                           It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new TodoListDocument());

      await _todoService.UpdateTodoListAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);

      _documentClientMock.Verify();
    }

    [TestMethod]
    public async Task CreateTodoListTaskAsync()
    {
      var requestDto = new CreateTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = TodoServiceTest.GetRandomToken(),
        Description = TodoServiceTest.GetRandomToken(),
        Deadline = DateTime.UtcNow.AddDays(20),
      };
      var todoListDocument = new TodoListDocument
      {
        Id = requestDto.TodoListId,
      };
      var userDocument = new UserDocument();

      _documentClientMock.Setup(client => client.UpdateAsync(
                           It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new TodoListDocument());

      await _todoService.CreateTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);

      _documentClientMock.Verify();
    }

    [TestMethod]
    public async Task UpdateTodoListTaskAsync()
    {
      var requestDto = new UpdateTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = TodoServiceTest.GetRandomToken(),
        Description = TodoServiceTest.GetRandomToken(),
        Deadline = DateTime.UtcNow.AddDays(20),
      };
      var todoListDocument = new TodoListDocument
      {
        Id = requestDto.TodoListId,
        Tasks = new[]
        {
          new TodoListTaskDocument
          {
            TaskId = requestDto.TodoListTaskId,
          },
        },
      };
      var userDocument = new UserDocument();

      await _todoService.UpdateTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);

      _documentClientMock.Verify();
    }

    [TestMethod]
    public async Task CompleteTodoListTaskAsync()
    {
      var requestDto = new CompleteTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };
      var todoListDocument = new TodoListDocument
      {
        Id = requestDto.TodoListId,
        Tasks = new[]
        {
          new TodoListTaskDocument
          {
            TaskId = requestDto.TodoListTaskId,
          },
        },
      };
      var userDocument = new UserDocument();

      await _todoService.CompleteTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);

      _documentClientMock.Verify();
    }

    private static string GetRandomToken() =>
      Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);
  }
}
