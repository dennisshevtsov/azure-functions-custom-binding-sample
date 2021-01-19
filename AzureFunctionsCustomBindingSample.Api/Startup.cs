// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFunctionsCustomBindingSample.Api.Startup))]

namespace AzureFunctionsCustomBindingSample.Api
{
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsCustomBindingSample.Api.Binding.Authorization;
  using AzureFunctionsCustomBindingSample.Api.Binding.Document;
  using AzureFunctionsCustomBindingSample.Api.Binding.Request;
  using AzureFunctionsCustomBindingSample.Api.Binding.Service;
  using AzureFunctionsCustomBindingSample.Validation;
  using AzureFunctionsCustomBindingSample.Validators;

  /// <summary>Provides an entry point to configure the function app.</summary>
  public sealed class Startup : IWebJobsStartup
  {
    /// <summary>Configures the function app.</summary>
    /// <param name="builder">An object that provides a simple API to configure the function app.</param>
    public void Configure(IWebJobsBuilder builder)
    {
      builder.AddExtension<AuthorizationExtensionConfigProvider>();
      builder.AddExtension<DocumentExtensionConfigProvider>();
      builder.AddExtension<RequestExtensionConfigProvider>();
      builder.AddExtension<ServiceExtensionConfigProvider>();

      builder.AddValidation(provider =>
      {
        provider.AddValidator<CreateProductValidator>("api/product", "post");
        provider.AddValidator<CreateOrderValidator>("api/order", "post");
      });

      builder.Services.AddDocumentClient(options =>
      {
        options.AccountEndpoint = "";
        options.AccountKey = "";
        options.DatabaseId = "";
        options.ContainerId = "";
        options.ItemsPerRequest = 10;
      });
      builder.Services.AddServices();
    }
  }
}
