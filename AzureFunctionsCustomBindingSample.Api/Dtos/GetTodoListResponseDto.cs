// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;
  using System.Collections.Generic;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class GetTodoListResponseDto
  {
    /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of TODO list tasks.</summary>
    public IEnumerable<GetTodoListTaskResponseDto> Tasks { get; set; }

    /// <summary>Represents detail of a TODO list task.</summary>
    public sealed class GetTodoListTaskResponseDto
    {
      /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
      public Guid TodoListTaskId { get; set; }

      /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
      public string Title { get; set; }
    }
  }
}
