﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class CreateTodoListResponseDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }
  }
}
