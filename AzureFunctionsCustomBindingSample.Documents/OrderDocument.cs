// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  public sealed class OrderDocument : DocumentBase
  {
    public float TotalPrice { get; set; }

    public IEnumerable<OrderProductDocument> Products { get; set; }

    public static OrderDocument New(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductDocument> productDocumentDictionary,
      UserDocument userDocument)
    {
      var orderProductDocuments = new List<OrderProductDocument>();
      var orderDocument = new OrderDocument
      {
        Products = orderProductDocuments,
      };
      var totalPrice = 0F;

      foreach (var productPair in productDocumentDictionary)
      {
        var orderProductDocument = new OrderProductDocument();

        orderProductDocument.ProductId = productPair.Key;
        orderProductDocument.Name = productPair.Value.Name;
        orderProductDocument.Units = productPair.Value.Units;
        orderProductDocument.Unit = productPair.Value.Unit;
        orderProductDocument.PricePerUnit = productPair.Value.PricePerUnit;
        orderProductDocument.TotalPrice = productPair.Value.PricePerUnit * products[productPair.Key];

        totalPrice += orderProductDocument.TotalPrice;

        orderProductDocuments.Add(orderProductDocument);
      }

      orderDocument.TotalPrice = totalPrice;

      return orderDocument;
    }
  }
}
