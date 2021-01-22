// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  /// <summary>Represents a simple API that provides operations to execute within the <see cref="Microsoft.Azure.WebJobs.Host.Bindings.BindingContext"/> class.</summary>
  public static class BindingContextExtensions
  {
    /// <summary>Tries to get an HTTP request from the <see cref="Microsoft.Azure.WebJobs.Host.Bindings.BindingContext"/> class.</summary>
    /// <param name="bindingContext">An object that represents a binding context.</param>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <returns>A value that indicates if there is an HTTP request in an instance of the <see cref="Microsoft.Azure.WebJobs.Host.Bindings.BindingContext"/> class.</returns>
    public static bool TryGetHttpRequest(this BindingContext bindingContext, out HttpRequest httpRequest)
    {
      if (bindingContext != null && bindingContext.BindingData != null)
      {
        foreach (var bindingDataElement in bindingContext.BindingData)
        {
          if (bindingDataElement.Value is HttpRequest httpRequestBindingElementValue)
          {
            httpRequest = httpRequestBindingElementValue;

            return true;
          }
        }
      }

      httpRequest = null;

      return false;
    }
  }
}
