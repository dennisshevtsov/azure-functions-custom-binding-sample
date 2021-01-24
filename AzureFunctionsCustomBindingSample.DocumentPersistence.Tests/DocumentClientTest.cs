// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence.Tests
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  [TestClass]
  public sealed class DocumentClientTest
  {
    private IDisposable _disposable;
    private IDocumentClient _documentClient;

    [TestInitialize]
    public void Initialize()
    {
      var configuration =
        new ConfigurationBuilder().AddJsonFile("local.settings.json")
                                  .Build();
      var provider =
        new ServiceCollection().AddDocumentClient(
                                  options =>
                                  {
                                    options.AccountEndpoint = configuration[nameof(DocumentClientOptions.AccountEndpoint)];
                                    options.AccountKey = configuration[nameof(DocumentClientOptions.AccountKey)];
                                    options.DatabaseId = configuration[nameof(DocumentClientOptions.DatabaseId)];
                                    options.ContainerId = configuration[nameof(DocumentClientOptions.ContainerId)];
                                  })
                               .BuildServiceProvider();

      _disposable = provider;
      _documentClient = provider.GetRequiredService<IDocumentClient>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [Ignore]
    [TestMethod]
    public async Task Test_FirstOrDefaultAsync()
    {
      var creating = DocumentClientTest.NewDocument();

      var created = await _documentClient.InsertAsync(creating, CancellationToken.None);

      var gotten = await _documentClient.FirstOrDefaultAsync<TestDocument>
        (created.Id, created.Type, CancellationToken.None);

      Assert.IsNotNull(gotten);

      Assert.AreEqual(created.ResourceId, gotten.ResourceId);
      Assert.AreEqual(created.SelfLink, gotten.SelfLink);
      Assert.AreEqual(created.Etag, gotten.Etag);
      Assert.AreEqual(created.AttachmentsLink, gotten.AttachmentsLink);
      Assert.AreEqual(created.Timestamp, gotten.Timestamp);

      Assert.AreEqual(created.StringProperty, gotten.StringProperty);
      Assert.AreEqual(created.DateTimeProperty, gotten.DateTimeProperty);
      Assert.AreEqual(created.GuidProperty, gotten.GuidProperty);

      Assert.IsNotNull(gotten.EmbeddedProperty);
      Assert.AreEqual(created.EmbeddedProperty.StringProperty, gotten.EmbeddedProperty.StringProperty);
      Assert.AreEqual(created.EmbeddedProperty.DateTimeProperty, gotten.EmbeddedProperty.DateTimeProperty);
      Assert.AreEqual(created.EmbeddedProperty.GuidProperty, gotten.EmbeddedProperty.GuidProperty);
    }

    [Ignore]
    [TestMethod]
    public async Task Test_InsertAsync()
    {
      var creating = DocumentClientTest.NewDocument();

      var created = await _documentClient.InsertAsync(creating, CancellationToken.None);

      Assert.IsNotNull(created);

      Assert.IsFalse(string.IsNullOrWhiteSpace(created.ResourceId));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.SelfLink));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.Etag));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.AttachmentsLink));
      Assert.IsTrue(created.Timestamp != default);

      Assert.AreEqual(creating.StringProperty, created.StringProperty);
      Assert.AreEqual(creating.DateTimeProperty, created.DateTimeProperty);
      Assert.AreEqual(creating.GuidProperty, created.GuidProperty);

      Assert.IsNotNull(created.EmbeddedProperty);
      Assert.AreEqual(creating.EmbeddedProperty.StringProperty, created.EmbeddedProperty.StringProperty);
      Assert.AreEqual(creating.EmbeddedProperty.DateTimeProperty, created.EmbeddedProperty.DateTimeProperty);
      Assert.AreEqual(creating.EmbeddedProperty.GuidProperty, created.EmbeddedProperty.GuidProperty);
    }

    private static TestDocument NewDocument()
      => new TestDocument
      {
        StringProperty = "test0",
        DateTimeProperty = DateTime.UtcNow,
        GuidProperty = Guid.NewGuid(),
        EmbeddedProperty = new EmbeddedTestDocument
        {
          StringProperty = "test1",
          DateTimeProperty = DateTime.UtcNow,
          GuidProperty = Guid.NewGuid(),
        },
      };
  }
}
