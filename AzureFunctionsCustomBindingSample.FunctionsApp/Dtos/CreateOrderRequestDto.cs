﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApp.Dtos
{
  using System;
  using System.Collections.Generic;

  public sealed class CreateOrderRequestDto
  {
    public IDictionary<Guid, int> Products { get; set; }
  }
}
