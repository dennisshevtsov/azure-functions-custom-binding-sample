// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  /// <summary>Represents a document node of Cosmos DB.</summary>
  /// <typeparam name="TEntity">A type of an entity that it stores in Cosmos DB.</typeparam>
  public sealed class Document<TEntity> where TEntity : class
  {
  }
}
