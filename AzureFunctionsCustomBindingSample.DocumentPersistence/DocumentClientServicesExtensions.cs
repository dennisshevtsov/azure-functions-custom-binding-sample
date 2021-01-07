// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System;

  using Microsoft.Azure.Cosmos;
  using Microsoft.Extensions.Options;
  using Microsoft.IO;

  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  public static class DocumentClientServicesExtensions
  {
    public static IServiceCollection AddDocumentClient(
      this IServiceCollection services,
      Action<DocumentClientOptions> configure)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      services.AddScoped(provider =>
      {
        var options = provider.GetRequiredService<IOptions<DocumentClientOptions>>()
                              .Value;
        var cosmosClient = new CosmosClient(options.AccountEndpoint, options.AccountKey);

        return cosmosClient;
      });
      services.AddScoped(provider =>
      {
        var options = provider.GetRequiredService<IOptions<DocumentClientOptions>>()
                              .Value;
        var container = provider.GetRequiredService<CosmosClient>()
                                .GetContainer(options.DatabaseId, options.ContainerId);

        return container;
      });

      services.AddSingleton<RecyclableMemoryStreamManagerProvider>();
      services.AddScoped(provider => Serializer.Get());
      services.AddScoped<IDocumentClient, DocumentClient>();
      services.Configure(configure);

      return services;
    }
  }
}
