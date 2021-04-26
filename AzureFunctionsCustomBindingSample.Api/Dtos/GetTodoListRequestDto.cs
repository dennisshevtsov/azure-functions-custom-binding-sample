// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  /// <summary>Represents conditions to query a TODO list.</summary>
  public sealed class GetTodoListRequestDto : ITodoListIdentity
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }
  }
}
