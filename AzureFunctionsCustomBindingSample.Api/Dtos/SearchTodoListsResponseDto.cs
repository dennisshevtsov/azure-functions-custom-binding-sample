﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;
  using System.Collections.Generic;

  /// <summary>Represents detail of TODO list records.</summary>
  public sealed class SearchTodoListsResponseDto
  {
    /// <summary>Gets/sets an object that represents a collection of TODO list records.</summary>
    public IEnumerable<SearchTodoListsItemResponseDto> Items { get; set; }

    /// <summary>Represents detail of a TODO list.</summary>
    public sealed class SearchTodoListsItemResponseDto : ITodoListIdentity
    {
      #region Members of ITodoListIdentity

      /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
      public Guid TodoListId { get; set; }

      #endregion

      /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
      public string Title { get; set; }
    }
  }
}
