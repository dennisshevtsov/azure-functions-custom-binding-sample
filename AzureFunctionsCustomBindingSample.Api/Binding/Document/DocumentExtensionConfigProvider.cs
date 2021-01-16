﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Document
{
  using Microsoft.Azure.WebJobs.Host.Config;

  /// <summary>Provides a simple API to register the document binding.</summary>
  public sealed class DocumentExtensionConfigProvider : IExtensionConfigProvider
  {
    /// <summary>Registers the document binding.</summary>
    /// <param name="context">An object that represents an extension config context.</param>
    public void Initialize(ExtensionConfigContext context) => context.AddBindingRule<DocumentAttribute>()
                                                                     .Bind(new DocumentBindingProvider());
  }
}
