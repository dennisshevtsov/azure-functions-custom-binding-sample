﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Api.Services;
  using AzureFunctionsCustomBindingSample.Binding.Authorization;
  using AzureFunctionsCustomBindingSample.Binding.Request;
  using AzureFunctionsCustomBindingSample.Binding.Service;
  using AzureFunctionsCustomBindingSample.Binding.Validation;
  using AzureFunctionsCustomBindingSample.Documents;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  public static class CreateTodoListTaskFunction
  {
    public static async Task<CreateTodoListTaskResponseDto> ExecuteAsync(
      [HttpTrigger("post", Route = "todo")] HttpRequest httpRequest,
      [Request] CreateTodoListTaskRequestDto requestDto,
      [Authorization] UserDocument userDocument,
      [Validation(ThrowIfInvalid = true)] ValidationResult validationResult,
      [Service] ITodoService service,
      CancellationToken cancellationToken)
      => await service.CreateTodoListTaskAsync(requestDto, userDocument, cancellationToken);
  }
}