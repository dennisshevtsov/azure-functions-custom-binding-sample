// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  using System.Threading;
  using System.Threading.Tasks;

  public interface IQueryHandler<TQuery>
  {
    public Task<object> HandleAsync(TQuery query, CancellationToken cancellationToken);
  }
}
