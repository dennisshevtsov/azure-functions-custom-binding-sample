// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Documents
{
  using AzureFunctionsCustomBindingSample.DocumentPersistence;

  /// <summary>Represents detail of a user.</summary>
  public sealed class UserDocument : DocumentBase
  {
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public UserAddressDocument Address { get; set; }
  }
}
