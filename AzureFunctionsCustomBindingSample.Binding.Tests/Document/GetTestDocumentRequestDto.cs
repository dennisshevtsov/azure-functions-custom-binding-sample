// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document.Tests
{
  using System;

  public sealed class GetTestDocumentRequestDto
  {
    public Guid TestDocumentId { get; set; }
  }
}
