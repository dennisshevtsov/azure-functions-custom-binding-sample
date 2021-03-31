// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  /// <summary>Represents detail of a task of a TODO list.</summary>
  public sealed class CreateTodoListTaskResponseDto : ITodoListTaskIdentity
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
