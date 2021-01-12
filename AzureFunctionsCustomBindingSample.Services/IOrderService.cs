﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Documents;

  /// <summary>Provides a simpe API to operate within instances of the <see cref="AzureFunctionsCustomBindingSample.Documents.OrderDocument"/> class.</summary>
  public interface IOrderService
  {
    /// <summary>Creates a new order.</summary>
    /// <param name="products">An object that represents a collection of pairs: a product and quantity.</param>
    /// <param name="productDocumentDictionary">An object that represents a collection of pairs: ID and a product. That exist in the DB.</param>
    /// <param name="userDocument">An object that represents a user that created an order.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<OrderDocument> CreateOrderAsync(
      IDictionary<Guid, int> products,
      IDictionary<Guid, ProductDocument> productDocumentDictionary,
      UserDocument userDocument,
      CancellationToken cancellationToken);
  }
}