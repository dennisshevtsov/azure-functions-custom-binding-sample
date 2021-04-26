// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Documents
{
  using System;

  public sealed class TodoListTaskDocument
  {
    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Deadline { get; set; }

    public bool Completed { get; set; }
  }
}
