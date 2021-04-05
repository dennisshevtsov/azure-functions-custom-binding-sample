// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;

  public interface IQueryResolver
  {
    public Task<TDocument> ResolveOneAsync<TDocument>(HttpRequest httpRequest, CancellationToken cancellationToken);

    public Task<IEnumerable<TDocument>> ResolveManyAsync<TDocument>(HttpRequest httpRequest, CancellationToken cancellationToken);
  }
}
