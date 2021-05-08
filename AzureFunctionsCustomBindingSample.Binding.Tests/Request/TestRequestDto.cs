// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Request.Tests
{
  using System;

  public sealed class TestRequestDto
  {
    public Guid TestId { get; set; }

    public Guid GuidProperty { get; set; }

    public int IntProperty { get; set; }

    public string StringProperty { get; set; }
  }
}
