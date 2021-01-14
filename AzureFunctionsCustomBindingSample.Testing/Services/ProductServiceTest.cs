﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Testing.Services
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using AzureFunctionsCustomBindingSample.Dtos;
  using AzureFunctionsCustomBindingSample.Services;

  [TestClass]
  public sealed class ProductServiceTest
  {
    private ProductService _productService;

    [TestInitialize]
    public void Initialize()
    {
      _productService = new ProductService();
    }

    [TestMethod]
    public async Task Test()
    {
      var command = new CreateProductRequestDto();

      await _productService.CreateProductAsync(command, CancellationToken.None);
    }
  }
}
