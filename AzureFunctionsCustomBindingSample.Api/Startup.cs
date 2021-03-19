// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFunctionsCustomBindingSample.Api.Startup))]

namespace AzureFunctionsCustomBindingSample.Api
{
  using System;

  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsCustomBindingSample.Api.Services;
  using AzureFunctionsCustomBindingSample.Binding.Authorization;
  using AzureFunctionsCustomBindingSample.Binding.Document;
  using AzureFunctionsCustomBindingSample.Binding.Request;
  using AzureFunctionsCustomBindingSample.Binding.Service;
  using AzureFunctionsCustomBindingSample.CosmosDb;

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

      builder.Services.AddDocumentClient(options =>
      {
        options.AccountEndpoint = Environment.GetEnvironmentVariable(nameof(DocumentClientOptions.AccountEndpoint));
        options.AccountKey = Environment.GetEnvironmentVariable(nameof(DocumentClientOptions.AccountKey));
        options.DatabaseId = Environment.GetEnvironmentVariable(nameof(DocumentClientOptions.DatabaseId));
        options.ContainerId = Environment.GetEnvironmentVariable(nameof(DocumentClientOptions.ContainerId));

        if (int.TryParse(Environment.GetEnvironmentVariable(nameof(DocumentClientOptions.ItemsPerRequest)), out var itemsPerRequest))
        {
          options.ItemsPerRequest = itemsPerRequest;
        }
        else
        {
          options.ItemsPerRequest = 10;
        }
      });
      builder.Services.AddScoped<ITodoService, TodoService>();
    }
  }
}
