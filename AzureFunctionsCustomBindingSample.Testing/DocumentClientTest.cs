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
                                    options.ContainerId = "orderdb";
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
      var productEntity = ProductEntity.New("test product", "test test test", 100, DateTime.UtcNow);
      var productDocument = Document.New(productEntity);

      Assert.IsTrue(productDocument.Id != default);
      Assert.AreEqual(nameof(ProductEntity), productDocument.PartitionKey);

      productDocument = await _documentClient.InsertAsync(productDocument, CancellationToken.None);

      Assert.IsNotNull(productDocument);
      Assert.IsFalse(string.IsNullOrWhiteSpace(productDocument.Rid));
      Assert.IsFalse(string.IsNullOrWhiteSpace(productDocument.Self));
      Assert.IsFalse(string.IsNullOrWhiteSpace(productDocument.Etag));
      Assert.IsFalse(string.IsNullOrWhiteSpace(productDocument.Attachments));
      Assert.IsTrue(productDocument.Ts != default);
    }
  }
}
