﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  public sealed class UserAddressDocument
  {
    public string AddressLine { get; set; }

    public string Zip { get; set; }

    public string City { get; set; }
  }
}
