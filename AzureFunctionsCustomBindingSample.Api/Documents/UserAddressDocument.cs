// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Documents
{
  public sealed class UserAddressDocument
  {
    public string AddressLine { get; set; }

    public string Zip { get; set; }

    public string City { get; set; }
  }
}
