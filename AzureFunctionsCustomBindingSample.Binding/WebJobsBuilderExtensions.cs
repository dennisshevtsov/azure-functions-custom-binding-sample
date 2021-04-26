// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsCustomBindingSample.Binding.Validation;
  using AzureFunctionsCustomBindingSample.Binding.Authorization;

  /// <summary>Provides a simple API to register services.</summary>
  public static class WebJobsBuilderExtensions
  {
    /// <summary>Registers the athorization buinding.</summary>
    /// <param name="builder">An object that provides a simple API to configure Azure functions.</param>
    /// <param name="authorize">An object that represents a method to get an authorized user for an HTTP request.</param>
    /// <returns>An object that provides a simple API to configure Azure functions.</returns>
    public static IWebJobsBuilder AddAuthorization(
      this IWebJobsBuilder builder,
      Func<HttpRequest, CancellationToken, Task<object>> authorize)
    {
      if (builder == null)
      {
        throw new ArgumentNullException(nameof(builder));
      }

      if (authorize == null)
      {
        throw new ArgumentNullException(nameof(authorize));
      }

      var provider = new AuthorizedUserProvider(authorize);

      builder.AddExtension(new AuthorizationExtensionConfigProvider(provider));

      return builder;
    }
  }
}
