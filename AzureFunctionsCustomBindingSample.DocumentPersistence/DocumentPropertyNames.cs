// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

using System.Collections.Generic;

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  /// <summary>Represents a set of document base property names.</summary>
  public static class DocumentPropertyNames
  {
    /// <summary>A value that represents a name of the ID property.</summary>
    public const string IdPropertyName = "id";

    /// <summary>A value that represents a name of the Partition Key property.</summary>
    public const string PartitionKeyPropertyName = "_type";

    /// <summary>A value that represents a name of the RID property.</summary>
    public const string RidPropertyName = "_rid";

    /// <summary>A value that represents a name of the Self property.</summary>
    public const string SelfPropertyName = "_self";

    /// <summary>A value that represents a name of the Etag property.</summary>
    public const string EtagPropertyName = "_etag";

    /// <summary>A value that represents a name of the Attachments property.</summary>
    public const string AttachmentsPropertyName = "_attachments";

    /// <summary>A value that represents a name of the TS property.</summary>
    public const string TsPropertyName = "_ts";

    /// <summary>An object that represents a dictionary of base document properties.</summary>
    public static readonly IDictionary<string, string> TypeJsonNameDictionary =
      new Dictionary<string, string>
      {
        { nameof(DocumentBase.Id), DocumentPropertyNames.IdPropertyName },
        { nameof(DocumentBase.Type), DocumentPropertyNames.PartitionKeyPropertyName },
        { nameof(DocumentBase.Rid), DocumentPropertyNames.RidPropertyName },
        { nameof(DocumentBase.Self), DocumentPropertyNames.SelfPropertyName },
        { nameof(DocumentBase.Etag), DocumentPropertyNames.EtagPropertyName },
        { nameof(DocumentBase.Attachments), DocumentPropertyNames.AttachmentsPropertyName },
        { nameof(DocumentBase.Ts), DocumentPropertyNames.TsPropertyName },
      };
  }
}
