// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Documents
{
  using AzureFunctionsCustomBindingSample.CosmosDb;

  /// <summary>Represents detail of a user.</summary>
  public sealed class UserDocument : DocumentBase
  {
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public UserAddressDocument Address { get; set; }
  }
}
