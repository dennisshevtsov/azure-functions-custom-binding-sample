// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.FunctionsApi.Binding
{
  using System;

  public sealed class User
  {
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }
  }
}
