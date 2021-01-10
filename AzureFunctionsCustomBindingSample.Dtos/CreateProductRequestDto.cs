// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Dtos
{
  using System;

  public sealed class CreateProductRequestDto
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public float PricePerUnit { get; set; }

    public Guid Unit { get; set; }
  }
}
