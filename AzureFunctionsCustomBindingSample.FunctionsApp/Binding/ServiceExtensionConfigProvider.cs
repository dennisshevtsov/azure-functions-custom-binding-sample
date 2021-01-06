﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Binding
{
  using Microsoft.Azure.WebJobs.Host.Config;

  public sealed class ServiceExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<ServiceAttribute>()
                                                                     .Bind(new ServiceBindingProvider());
  }
}