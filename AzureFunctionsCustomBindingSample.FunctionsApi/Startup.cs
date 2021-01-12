// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFunctionsCustomBindingSample.FunctionsApi.Startup))]

namespace AzureFunctionsCustomBindingSample.FunctionsApi
{
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Authorization;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Entity;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Request;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Service;
  using AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Validation;
  using AzureFunctionsCustomBindingSample.Services;

  public sealed class Startup : IWebJobsStartup
  {
    public void Configure(IWebJobsBuilder builder)
    {
      builder.AddExtension<AuthorizationExtensionConfigProvider>();
      builder.AddExtension<EntityExtensionConfigProvider>();
      builder.AddExtension<RequestExtensionConfigProvider>();
      builder.AddExtension<ServiceExtensionConfigProvider>();
      builder.AddExtension<ValidationExtensionConfigProvider>();

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
