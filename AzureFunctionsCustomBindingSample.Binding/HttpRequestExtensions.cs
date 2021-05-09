// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Binding.Request;

  /// <summary>Extends the API of the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> class.</summary>
  public static class HttpRequestExtensions
  {
    /// <summary>Stores a request DTO in a request scope.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="requestDto">An object that represents parameters of a request.</param>
    public static void SetRequestDto(this HttpRequest httpRequest, object requestDto)
    {
      if (httpRequest == null)
      {
        throw new ArgumentNullException(nameof(httpRequest));
      }

      httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] = requestDto;
    }

    /// <summary>Gets a request DTO.</summary>
    /// <typeparam name="TRequestDto">A type of a request DTO.</typeparam>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <returns>An object that represents parameters of a request.</returns>
    public static TRequestDto GetRequestDto<TRequestDto>(this HttpRequest httpRequest)
      where TRequestDto : class
    {
      if (httpRequest == null)
      {
        throw new ArgumentNullException(nameof(httpRequest));
      }

      var requestDto = httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] as TRequestDto;

      return requestDto;
    }

    /// <summary>Gets a request DTO.</summary>
    /// <param name="httpRequest">An object that represents parameters of a request.</param>
    /// <returns>An object that represents parameters of a request.</returns>
    public static object GetRequestDto(this HttpRequest httpRequest)
    {
      if (httpRequest == null)
      {
        throw new ArgumentNullException(nameof(httpRequest));
      }

      var requestDto = httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName];

      return requestDto;
    }
  }
}
