// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Testing
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using AzureFunctionsCustomBindingSample.EntityModel;
  using AzureFunctionsCustomBindingSample.RepositoryModel;

  [TestClass]
  public sealed class DocumentClientTest
  {
    private IDisposable _disposable;
    private IDocumentClient _documentClient;

    [TestInitialize]
    public void Initialize()
    {
      var provider =
        new ServiceCollection().AddDocumentClient(
                                  options =>
                                  {
                                    options.AccountEndpoint = "https://localhost:8081";
                                    options.AccountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                                    options.DatabaseId = "afcbs";
                                    options.ContainerId = "order-db";
                                  })
                               .BuildServiceProvider();

      _disposable = provider;
      _documentClient = provider.GetRequiredService<IDocumentClient>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [TestMethod]
    public async Task Test()
    {
      var orderEntity = new OrderEntity
      {
        OrderId = Guid.NewGuid(),
      };

      var orderEntityDocument = await _documentClient.InsertAsync(orderEntity, nameof(OrderEntity), CancellationToken.None);

    }
  }
}
