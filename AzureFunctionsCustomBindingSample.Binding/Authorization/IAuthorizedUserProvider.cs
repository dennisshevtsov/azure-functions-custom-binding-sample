// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;

  /// <summary>Provides a simple API to get an authorized user.</summary>
  public interface IAuthorizedUserProvider
  {
    /// <summary>Gets an authorized user for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetAuthorizedUserAsync(
      HttpRequest httpRequest, CancellationToken cancellationToken);
  }
}
