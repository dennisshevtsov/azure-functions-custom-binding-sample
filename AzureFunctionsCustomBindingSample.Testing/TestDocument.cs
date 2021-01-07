using AzureFunctionsCustomBindingSample.DocumentPersistence;
using System;

namespace AzureFunctionsCustomBindingSample.Testing
{
  public sealed class TestDocument : DocumentBase
  {
    public string StringProperty { get; set; }

    public DateTime DateTimeProperty { get; set; }

    public Guid GuidProperty { get; set; }

    public EmbeddedTestDocument EmbeddedProperty { get; set; }
  }

  public sealed class EmbeddedTestDocument
  {
    public string StringProperty { get; set; }

    public DateTime DateTimeProperty { get; set; }

    public Guid GuidProperty { get; set; }
  }
}
