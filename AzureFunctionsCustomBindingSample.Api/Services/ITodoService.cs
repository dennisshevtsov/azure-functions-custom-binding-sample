// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Documents;

  /// <summary>Provides a simple API to execute operation within the TODO list domain.</summary>
  public interface ITodoService
  {
    /// <summary></summary>
    /// <param name="requestDto">An object that r</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CreateTodoListResponseDto> CreateTodoListAsync(
      CreateTodoListRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto">An object that r</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<UpdateTodoListResponseDto> UpdateTodoListAsync(
      UpdateTodoListRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto">An object that r</param>
    /// <param name="userDocument">An object that r</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(
      CreateTodoListTaskRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto">An object that r</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<UpdateTodoListTaskResponseDto> UpdateTodoListTaskAsync(
      UpdateTodoListTaskRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto">An object that r</param>
    /// <param name="userDocument">An object that represents detail of a user.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<CompleteTodoListTaskResponseDto> CompleteTodoListTaskAsync(
      CompleteTodoListTaskRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);
  }
}
