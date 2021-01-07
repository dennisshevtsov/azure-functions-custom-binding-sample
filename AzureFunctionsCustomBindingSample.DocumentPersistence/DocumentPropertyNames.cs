// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

using System.Collections.Generic;

namespace AzureFunctionsCustomBindingSample.DocumentPersistence
{
  public static class DocumentPropertyNames
  {
    public const string IdPropertyName = "id";

    public const string PartitionKeyPropertyName = "_type";

    public const string RidPropertyName = "_rid";

    public const string SelfPropertyName = "_self";

    public const string EtagPropertyName = "_etag";

    public const string AttachmentsPropertyName = "_attachments";

    public const string TsPropertyName = "_ts";

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
