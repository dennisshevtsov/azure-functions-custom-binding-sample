// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services.Tests
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

  [TestClass]
  public sealed class ProductServiceTest
  {
    private Mock<IDocumentClient> _documentClientMock;
    private ProductService _productService;

    [TestInitialize]
    public void Initialize()
    {
      _documentClientMock = new Mock<IDocumentClient>();
      _productService = new ProductService(_documentClientMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      Setup();

      var command = ProductServiceTest.GetCommand();
      var unitDocument = ProductServiceTest.GetUnit();

      var productDocument = await _productService.CreateProductAsync(
        command, unitDocument, CancellationToken.None);

      ProductServiceTest.Test(command, unitDocument, productDocument);
      _documentClientMock.Verify(client => client.InsertAsync(It.IsAny<ProductDocument>(), It.IsAny<CancellationToken>()));
    }

    private void Setup()
    {
      _documentClientMock.Setup(client => client.InsertAsync(It.IsAny<ProductDocument>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((ProductDocument productDocument, CancellationToken cancellationToken) => productDocument)
                         .Verifiable();
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");

    private static CreateProductRequestDto GetCommand()
      => new CreateProductRequestDto
      {
        Name = ProductServiceTest.RandomToken(),
        Description = ProductServiceTest.RandomToken(),
        PricePerUnit = 100,
      };

    private static UnitDocument GetUnit()
      => new UnitDocument
      {
        Id = Guid.NewGuid(),
        Name = ProductServiceTest.RandomToken(),
      };

    private static void Test(
      CreateProductRequestDto command, UnitDocument unitDocument, ProductDocument productDocument)
    {
      Assert.IsNotNull(productDocument);

      Assert.AreEqual(command.Name, productDocument.Name);
      Assert.AreEqual(command.Description, productDocument.Description);
      Assert.AreEqual(command.PricePerUnit, productDocument.PricePerUnit);
      Assert.AreEqual(false, productDocument.Enabled);

      Assert.IsNotNull(productDocument.Unit);

      Assert.AreEqual(unitDocument.Id, productDocument.Unit.UnitId);
      Assert.AreEqual(unitDocument.Name, productDocument.Unit.Name);
    }
  }
}
