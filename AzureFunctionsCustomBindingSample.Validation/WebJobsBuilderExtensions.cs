// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System;

  using Microsoft.Azure.WebJobs;

  public static class WebJobsBuilderExtensions
  {
    public static IWebJobsBuilder AddValidation(
      this IWebJobsBuilder builder,
      Action<ValidatorProvider> configure)
    {
      if (builder == null)
      {
        throw new ArgumentNullException(nameof(builder));
      }

      if (configure == null)
      {
        throw new ArgumentNullException(nameof(configure));
      }

      var provider = new ValidatorProvider();

      configure.Invoke(provider);

      builder.AddExtension(new ValidationExtensionConfigProvider(provider));

      return builder;
    }
  }
}
