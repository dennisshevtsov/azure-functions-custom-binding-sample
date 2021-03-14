// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Tests
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Api.Documents;
  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Services;
  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;

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
      var requestDto = new CreateTodoListRequestDto();
      var userDocument = new UserDocument();

      await _todoService.CreateTodoListAsync(requestDto, userDocument, CancellationToken.None);
    }

    [TestMethod]
    public async Task UpdateTodoListAsync()
    {
      var requestDto = new UpdateTodoListRequestDto();
      var todoListDocument = new TodoListDocument();
      var userDocument = new UserDocument();

      await _todoService.UpdateTodoListAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);
    }

    [TestMethod]
    public async Task CreateTodoListTaskAsync()
    {
      var requestDto = new CreateTodoListTaskRequestDto();
      var todoListDocument = new TodoListDocument();
      var userDocument = new UserDocument();

      await _todoService.CreateTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);
    }

    [TestMethod]
    public async Task UpdateTodoListTaskAsync()
    {
      var requestDto = new UpdateTodoListTaskRequestDto();
      var todoListDocument = new TodoListDocument();
      var userDocument = new UserDocument();

      await _todoService.UpdateTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);
    }

    [TestMethod]
    public async Task CompleteTodoListTaskAsync()
    {
      var requestDto = new CompleteTodoListTaskRequestDto();
      var todoListDocument = new TodoListDocument();
      var userDocument = new UserDocument();

      await _todoService.CompleteTodoListTaskAsync(requestDto, todoListDocument, userDocument, CancellationToken.None);
    }
  }
}
