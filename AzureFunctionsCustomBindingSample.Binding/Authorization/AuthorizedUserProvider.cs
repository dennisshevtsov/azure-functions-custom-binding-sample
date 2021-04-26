// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Authorization
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;

  /// <summary>Provides a simple API to get an authorized user.</summary>
  public sealed class AuthorizedUserProvider : IAuthorizedUserProvider
  {
    private readonly Func<HttpRequest, CancellationToken, Task<object>> _authorize;

    /// <summary>Initializes a new instance of the <see cref="AuthorizedUserProvider"/> class.</summary>
    /// <param name="authorize">An object that represents a method to get an authorized user for an HTTP request.</param>
    public AuthorizedUserProvider(Func<HttpRequest, CancellationToken, Task<object>> authorize)
      => _authorize = authorize ?? throw new ArgumentNullException(nameof(authorize));

    /// <summary>Gets an authorized user for an HTTP request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> GetAuthorizedUserAsync(
      HttpRequest httpRequest, CancellationToken cancellationToken)
      => _authorize.Invoke(httpRequest, cancellationToken);
  }
}
