// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services.Tests
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;
  using AzureFunctionsCustomBindingSample.Documents;
  using AzureFunctionsCustomBindingSample.Services;

  [TestClass]
  public sealed class OrderServiceTest
  {
    private Mock<IDocumentClient> _documentClientMock;
    private OrderService _orderService;

    [TestInitialize]
    public void Initialize()
    {
      _documentClientMock = new Mock<IDocumentClient>();
      _orderService = new OrderService(_documentClientMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var productId0 = Guid.NewGuid();
      var productId1 = Guid.NewGuid();
      var productId2 = Guid.NewGuid();

      var products = new Dictionary<Guid, int>
      {
        { productId0, 2 },
        { productId1, 1 },
        { productId2, 5 },
      };
      var productDocumentDictionary = new Dictionary<Guid, ProductDocument>
      {
        { productId0, OrderServiceTest.GetProduct(productId0, 10) },
        { productId1, OrderServiceTest.GetProduct(productId1, 15) },
        { productId2, OrderServiceTest.GetProduct(productId2, 5.5F) },
      };
      var userDocument = new UserDocument
      {
        Id = Guid.NewGuid(),
        Name = OrderServiceTest.RandomToken(),
      };

      var orderDocument = await _orderService.CreateOrderAsync(
        products, productDocumentDictionary, userDocument, CancellationToken.None);

      OrderServiceTest.Test(products, productDocumentDictionary, userDocument, orderDocument);
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");

    private static ProductDocument GetProduct(Guid productId, float pricePerUnit)
      => new ProductDocument
      {
        Id = productId,
        Sku = OrderServiceTest.RandomToken(),
        Name = OrderServiceTest.RandomToken(),
        Description = OrderServiceTest.RandomToken(),
        PricePerUnit = pricePerUnit,
        Unit = new ProductUnitDocument
        {
          UnitId = Guid.NewGuid(),
          Name = OrderServiceTest.RandomToken(),
        },
        Enabled = true,
      };

    private static void Test(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductDocument> productDocumentDictionary,
      UserDocument userDocument,
      OrderDocument orderDocument)
    {
      Assert.IsNotNull(orderDocument);

      Assert.IsTrue(string.IsNullOrWhiteSpace(orderDocument.OrderNo));

      var totalPrice = productDocumentDictionary.Sum(product => product.Value.PricePerUnit * products[product.Key]);
      Assert.AreEqual(totalPrice, orderDocument.TotalPrice);

      Assert.IsNotNull(orderDocument.Products);
      Assert.IsTrue(orderDocument.Products.Any());

      foreach (var product in orderDocument.Products)
      {
        Assert.AreEqual(productDocumentDictionary[product.ProductId].Name, product.Name);
        Assert.AreEqual(products[product.ProductId], product.Units);

        var unit = productDocumentDictionary[product.ProductId].Unit;
        Assert.IsNotNull(unit);
        Assert.AreEqual(unit.UnitId, product.Unit.UnitId);
        Assert.AreEqual(unit.Name, product.Unit.Name);

        Assert.AreEqual(productDocumentDictionary[product.ProductId].PricePerUnit, product.PricePerUnit);
        Assert.AreEqual(productDocumentDictionary[product.ProductId].PricePerUnit * products[product.ProductId], product.TotalPrice);
      }
    }
  }
}
