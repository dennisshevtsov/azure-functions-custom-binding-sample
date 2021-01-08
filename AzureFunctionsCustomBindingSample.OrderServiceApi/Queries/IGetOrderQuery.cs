// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.OrderServiceApi.Queries
{
  using System;

  public interface IGetOrderQuery
  {
    public Guid OrderId { get; set; }
  }
}
