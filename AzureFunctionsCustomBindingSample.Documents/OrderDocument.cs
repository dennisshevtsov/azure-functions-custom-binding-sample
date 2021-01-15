// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using System;
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  /// <summary>Represents detail of an order.</summary>
  public sealed class OrderDocument : DocumentBase
  {
    /// <summary>Gets/sets a value that represents an order #.</summary>
    public string OrderNo { get; set; }

    /// <summary>Gets/sets a value that represents a total price of an order.</summary>
    public float TotalPrice { get; set; }

    /// <summary>Gets/sets an object that represents a collection of ordered products.</summary>
    public IEnumerable<OrderProductDocument> Products { get; set; }

    /// <summary>Creates an instance of the <see cref="OrderDocument"/> class that represents a new order.</summary>
    /// <param name="products">An object that represents a dictionary of ordered product numbers.</param>
    /// <param name="productDocumentDictionary">An object that represents a dictionary of available products.</param>
    /// <param name="userDocument">An object that represents a user that creates an order.</param>
    /// <returns>An instance of the <see cref="OrderDocument"/> class.</returns>
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
