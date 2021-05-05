// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  [TestClass]
  public sealed class DocumentValueProviderTest
  {
    private Mock<IDocumentProvider> _documentProviderMock;
    private Mock<IServiceProvider> _serviceProviderMock;
    private Mock<HttpContext> _httpContextMock;
    private Mock<HttpRequest> _httpRequestMock;
    private DocumentValueProvider _valueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _documentProviderMock = new Mock<IDocumentProvider>();

      _serviceProviderMock = new Mock<IServiceProvider>();
      _serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                          .Returns(_documentProviderMock.Object);

      _httpContextMock = new Mock<HttpContext>();
      _httpContextMock.SetupGet(context => context.RequestServices)
                      .Returns(_serviceProviderMock.Object);

      _httpRequestMock = new Mock<HttpRequest>();
      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(_httpContextMock.Object);
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

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Documents()
    {
      var stringProperty = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper();
      var dateTimeProperty = DateTime.UtcNow;

      Setup(stringProperty, dateTimeProperty);

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var testDocuments = value as IEnumerable<TestDocument>;

      Assert.IsNotNull(testDocuments);

      foreach (var testDocument in testDocuments)
      {
        Assert.IsNotNull(testDocument);
        Assert.AreEqual(stringProperty, testDocument.StringProperty);
        Assert.AreEqual(dateTimeProperty, testDocument.DateTimeProperty);
      }
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
      _valueProvider = new DocumentValueProvider(typeof(TestDocument), _httpRequestMock.Object);
      _documentProviderMock.Setup(provider => provider.GetDocumentAsync(It.IsAny<HttpRequest>(), It.IsAny<Type>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync((HttpRequest httpRequest, Type type, CancellationToken CancellationToken) =>
                           {
                             object instance = null;

                             if (httpRequest.HttpContext.Items.TryGetValue(
                                   "__request__", out var requestObject) &&
                                 requestObject is GetTestDocumentRequestDto requestDto)
                             {
                               instance = new TestDocument
                               {
                                 Id = requestDto.TestDocumentId,
                               };
                             }

                             return instance;
                           });
    }

    private void Setup(string stringProperty, DateTime dateTimeProperty)
    {
      var items = new Dictionary<object, object>();

      var requestDto = new SearchTestDocumentsRequestDto
      {
        StringProperty = stringProperty,
        DateTimeProperty = dateTimeProperty,
      };
      items.Add("__request__", requestDto);

      _httpContextMock.SetupGet(context => context.Items)
                      .Returns(items);
      _valueProvider = new DocumentValueProvider(typeof(IEnumerable<TestDocument>), _httpRequestMock.Object);
      _documentProviderMock.Setup(provider => provider.GetDocumentsAsync(It.IsAny<HttpRequest>(), It.IsAny<Type>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync((HttpRequest httpRequest, Type type, CancellationToken CancellationToken) =>
                           {
                             object instance = null;

                             if (httpRequest.HttpContext.Items.TryGetValue(
                                   "__request__", out var requestObject) &&
                                 requestObject is SearchTestDocumentsRequestDto requestDto)
                             {
                               instance = Enumerable.Range(1, 5)
                                                    .Select(number => new TestDocument
                                                    {
                                                      StringProperty = requestDto.StringProperty,
                                                      DateTimeProperty = requestDto.DateTimeProperty,
                                                    });
                             }

                             return instance;
                           });
    }
  }
}
