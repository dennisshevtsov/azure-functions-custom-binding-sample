// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Entity
{
  using Microsoft.Azure.WebJobs.Host.Config;

  public sealed class DocumentExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<DocumentAttribute>()
                                                                     .Bind(new DocumentBindingProvider());
  }
}
