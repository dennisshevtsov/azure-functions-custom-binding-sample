
namespace AzureFunctionsCustomBindingSample.Testing.DocumentPersistence
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

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
    public async Task TestInsert()
    {
      var creating = new TestDocument
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

      var created = await _documentClient.InsertAsync(creating, CancellationToken.None);

      Assert.IsNotNull(created);

      Assert.IsFalse(string.IsNullOrWhiteSpace(created.Rid));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.Self));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.Etag));
      Assert.IsFalse(string.IsNullOrWhiteSpace(created.Attachments));
      Assert.IsTrue(created.Ts != default);

      Assert.AreEqual(creating.StringProperty, created.StringProperty);
      Assert.AreEqual(creating.DateTimeProperty, created.DateTimeProperty);
      Assert.AreEqual(creating.GuidProperty, created.GuidProperty);

      Assert.IsNotNull(created.EmbeddedProperty);
      Assert.AreEqual(creating.EmbeddedProperty.StringProperty, created.EmbeddedProperty.StringProperty);
      Assert.AreEqual(creating.EmbeddedProperty.DateTimeProperty, created.EmbeddedProperty.DateTimeProperty);
      Assert.AreEqual(creating.EmbeddedProperty.GuidProperty, created.EmbeddedProperty.GuidProperty);
    }
  }
}
