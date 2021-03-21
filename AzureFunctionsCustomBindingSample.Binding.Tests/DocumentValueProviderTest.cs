// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.CosmosDb;
  using AzureFunctionsCustomBindingSample.Binding.Document;

  [TestClass]
  public sealed class DocumentValueProviderTest
  {
    private Mock<IQueryHandler<GetTestDocumentRequestDto>> _queryHandlerMock;
    private Mock<IServiceProvider> _serviceProviderMock;
    private Mock<HttpContext> _httpContextMock;
    private Mock<HttpRequest> _httpRequestMock;
    private DocumentValueProvider _valueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _queryHandlerMock = new Mock<IQueryHandler<GetTestDocumentRequestDto>>();
      _queryHandlerMock.Setup(handler => handler.HandleAsync(It.IsAny<GetTestDocumentRequestDto>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((GetTestDocumentRequestDto requestDto, CancellationToken CancellationToken) =>
                       new TestDocument
                       {
                         Id = requestDto.TestDocumentId,
                       });

      _serviceProviderMock = new Mock<IServiceProvider>();
      _serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                          .Returns(_queryHandlerMock.Object);

      _httpContextMock = new Mock<HttpContext>();
      _httpContextMock.SetupGet(context => context.RequestServices)
                      .Returns(_serviceProviderMock.Object);

      _httpRequestMock = new Mock<HttpRequest>();
      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(_httpContextMock.Object);

      _valueProvider = new DocumentValueProvider(typeof(TestDocument), _httpRequestMock.Object);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Document_By_Its_ID()
    {
      var testId = Guid.NewGuid();

      Setup(testId);

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var testDocument = value as TestDocument;

      Assert.IsNotNull(testDocument);
      Assert.AreEqual(testId, testDocument.Id);
    }

    private void Setup(Guid testId)
    {
      var items = new Dictionary<object, object>();

      var requestDto = new GetTestDocumentRequestDto
      {
        TestDocumentId = testId,
      };
      items.Add("__request__", requestDto);

      _httpContextMock.SetupGet(context => context.Items)
                      .Returns(items);
    }
  }
}
