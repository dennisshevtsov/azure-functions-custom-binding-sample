// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Testing.Binding
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.Api.Binding.Document;
  using AzureFunctionsCustomBindingSample.Api.Binding.Request;
  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;

  [TestClass]
  public sealed class DocumentValueProviderTest
  {
    private Mock<HttpRequest> _httpRequestMock;
    private DocumentValueProvider _valueProvider;

    [TestInitialize]
    public void Initialize()
    {
      _httpRequestMock = new Mock<HttpRequest>();
      _valueProvider = new DocumentValueProvider(typeof(TestDocument), _httpRequestMock.Object);
    }

    [TestMethod]
    public async Task GetValueAsync_Should_Return_Document_By_Its_ID()
    {
      var productId = Guid.NewGuid();

      Setup(productId);

      var value = await _valueProvider.GetValueAsync();

      Assert.IsNotNull(value);

      var productDocument = value as ProductDocument;

      Assert.IsNotNull(productDocument);
      Assert.AreEqual(productId, productDocument.Id);
    }

    private void Setup(Guid productId)
    {
      var httpContextMock = new Mock<HttpContext>();

      var items = new Dictionary<object, object>();

      var requestDto = new GetProductRequestDto
      {
        ProductId = Guid.NewGuid(),
      };
      items.Add(RequestBinding.ParameterDescriptorName, requestDto);

      httpContextMock.SetupGet(context => context.Items)
                     .Returns(items);

      _httpRequestMock.SetupGet(request => request.HttpContext)
                      .Returns(httpContextMock.Object);

      var documentClientMock = new Mock<IDocumentClient>();

      documentClientMock.Setup(client => client.FirstOrDefaultAsync<ProductDocument>(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Guid id, string partitionId, CancellationToken cancellationToken) => new ProductDocument
                        {
                          Id = productId,
                          Type = partitionId,
                        });

      var serviceProviderMock = new Mock<IServiceProvider>();

      serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                         .Returns((Type type) =>
                         {
                           if (type == typeof(IDocumentClient))
                           {
                             return documentClientMock.Object;
                           }

                           return null;
                         });

      httpContextMock.Setup(context => context.RequestServices)
                     .Returns(serviceProviderMock.Object);
    }
  }
}
