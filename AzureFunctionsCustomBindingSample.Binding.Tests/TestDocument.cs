// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document.Tests
{
  using System;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

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
