﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Services
{
  using System;

  using Microsoft.Extensions.DependencyInjection;

  public static class ServicesExtensions
  {
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      if (services != null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<IOrderService, OrderService>();

      return services;
    }
  }
}