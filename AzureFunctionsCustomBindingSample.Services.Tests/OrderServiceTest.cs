// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services.Tests
{
  using System;
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
      var products = new Dictionary<Guid, int>();
      var productDocumentDictionary = new Dictionary<Guid, ProductDocument>();
      var userDocument = new UserDocument();

      await _orderService.CreateOrderAsync(products, productDocumentDictionary, userDocument, CancellationToken.None);
    }
  }
}
