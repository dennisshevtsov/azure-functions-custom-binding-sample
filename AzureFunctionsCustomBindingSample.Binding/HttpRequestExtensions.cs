// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;

  using Microsoft.AspNetCore.Http;

  using AzureFunctionsCustomBindingSample.Binding.Request;

  public static class HttpRequestExtensions
  {
    public static void SetRequestDto(this HttpRequest httpRequest, object requestDto)
    {
      if (httpRequest == null)
      {
        throw new ArgumentNullException(nameof(httpRequest));
      }

      httpRequest.HttpContext.Items[RequestBinding.ParameterDescriptorName] = requestDto;
    }

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
