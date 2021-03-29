// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a method to resolve a document with a query.</summary>
  /// <typeparam name="TQuery">A type of a query.</typeparam>
  public interface IQueryHandler<TQuery>
  {
    /// <summary>Resolves a document with a query.</summary>
    /// <param name="query">An object that represents a query.</param>
    /// <param name="cancellationToken">A value that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an async operation.</returns>
    public Task<object> HandleAsync(TQuery query, CancellationToken cancellationToken);
  }
}
