// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Dtos
{
  using System;

  public interface ITodoListTaskIdentity
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
