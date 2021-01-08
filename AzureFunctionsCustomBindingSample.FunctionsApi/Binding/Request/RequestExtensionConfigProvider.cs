// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Binding.Request
{
  using Microsoft.Azure.WebJobs.Host.Config;

  public sealed class RequestExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<RequestAttribute>()
                                                                     .Bind(new RequestBindingProvider());
  }
}
