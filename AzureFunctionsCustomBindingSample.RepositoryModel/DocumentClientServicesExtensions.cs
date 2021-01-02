using AzureFunctionsCustomBindingSample.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class DocumentClientServicesExtensions
  {
    public static IServiceCollection AddDocumentClient(
      this IServiceCollection services,
      Action<RequestOptions> configure)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      services.AddScoped<IDocumentClient, DocumentClient>();
      services.Configure(configure);

      return services;
    }
  }
}
