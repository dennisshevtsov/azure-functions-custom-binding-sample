// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Documents
{
  using System.Collections.Generic;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  public sealed class TodoListDocument : DocumentBase
  {
    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<TodoListTaskDocument> Tasks { get; set; }
  }
}
