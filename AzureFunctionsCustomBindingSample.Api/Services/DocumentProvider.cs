// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.CosmosDb;

  using Microsoft.AspNetCore.Http;

  /// <summary>Represents a simple API to query documents.</summary>
  public sealed class DocumentProvider : IDocumentProvider
  {
    private readonly IDocumentClient _documentClient;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Api.Services.DocumentProvider"/> class.</summary>
    /// <param name="documentClient">An object that provides a simple API to persistence of documents that inherits the <see cref="AzureFunctionsCustomBindingSample.CosmosDb.DocumentBase"/> class.</param>
    public DocumentProvider(IDocumentClient documentClient)
      => _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));

    /// <summary>Gets a single document for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="documentType">An object that represents a type of a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetDocumentAsync(HttpRequest httpRequest, Type documentType, CancellationToken cancellationToken)
    {
      var requestDto = httpRequest.HttpContext.Items["__request__"];

      if (requestDto is ITodoListIdentity identity)
      {
        var task = (Task)typeof(IDocumentClient).GetMethod(nameof(IDocumentClient.FirstOrDefaultAsync))
                                                .MakeGenericMethod(documentType)
                                                .Invoke(_documentClient,
                                                        new object[]
                                                        {
                                                          identity.TodoListId,
                                                          documentType.Name,
                                                          cancellationToken,
                                                        });
        var taskWithResult = task.ContinueWith(task => typeof(Task<>).MakeGenericType(documentType)
                                                                     .GetProperty(nameof(Task<object>.Result))
                                                                     .GetValue(task),
                                               cancellationToken);

        return taskWithResult;
      }

      return Task.FromResult(default(object));
    }

    /// <summary>Gets a collection of documents for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="documentType">An object that represents a type of a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetDocumentsAsync(HttpRequest httpRequest, Type documentType, CancellationToken cancellationToken)
      => Task.FromResult(default(object));
  }
}
