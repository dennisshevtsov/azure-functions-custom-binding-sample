// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  /// <summary>Provides json names of the document collection.</summary>
  public static class DocumentCollectionPropertyNames
  {
    /// <summary>A value that represents a JSON name of the documents property.</summary>
    public const string DocumentsPropertyName = "Documents";

    /// <summary>A value that represents a JSON name of the resource ID property.</summary>
    public const string ResourceIdPropertyName = "_rid";

    /// <summary>A value that represents a JSON name of the count property.</summary>
    public const string CountPropertyName = "_count";
  }
}
