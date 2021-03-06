﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Binding;
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.CosmosDb;

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
      var requestDto = httpRequest.GetRequestDto();

      var methodName = nameof(DocumentClientExtensions.FirstOrDefaultAsync);
      var type = typeof(DocumentClientExtensions);
      var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public)
                       .MakeGenericMethod(documentType);
      var parameters = new object[]
      {
        _documentClient,
        requestDto,
        cancellationToken,
      };
      var task = (Task)method.Invoke(null, parameters);
      var taskWithResult = task.ContinueWith(
        task => typeof(Task<>).MakeGenericType(documentType)
                              .GetProperty(nameof(Task<object>.Result))
                              .GetValue(task),
        cancellationToken);

      return taskWithResult;
    }

    /// <summary>Gets a collection of documents for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="documentType">An object that represents a type of a document.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetDocumentsAsync(HttpRequest httpRequest, Type documentType, CancellationToken cancellationToken)
    {
      var requestDto = httpRequest.GetRequestDto();

      var methodName = nameof(DocumentClientExtensions.ToListAsync);
      var type = typeof(DocumentClientExtensions);
      var types = new[]
      {
        typeof(IDocumentClient),
        typeof(IDocumentCollectionQuery),
        typeof(CancellationToken),
      };
      var method = type.GetMethod(methodName, types)
                       .MakeGenericMethod(documentType);
      var parameters = new object[]
      {
        _documentClient,
        requestDto,
        cancellationToken,
      };
      var task = (Task)method.Invoke(null, parameters);
      var taskWithResult = task.ContinueWith(
        task => typeof(Task<>).MakeGenericType(typeof(IList<>).MakeGenericType(documentType))
                              .GetProperty(nameof(Task<object>.Result))
                              .GetValue(task),
        cancellationToken);

      return taskWithResult;
    }
  }
}
