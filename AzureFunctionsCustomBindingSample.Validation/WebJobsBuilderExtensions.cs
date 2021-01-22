// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System;

  using Microsoft.Azure.WebJobs;

  /// <summary>Provides a simple API to register services.</summary>
  public static class WebJobsBuilderExtensions
  {
    /// <summary>Registers validation services.</summary>
    /// <param name="builder">An object that provides a simple API to configure Azure functions.</param>
    /// <param name="configure">An object that provides a simple API to bind a validator to an endpoint.</param>
    /// <returns>An object that provides a simple API to configure Azure functions.</returns>
    public static IWebJobsBuilder AddValidation(
      this IWebJobsBuilder builder,
      Action<IValidationConfig> configure)
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
