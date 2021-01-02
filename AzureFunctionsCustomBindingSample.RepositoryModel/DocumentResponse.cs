using System.Collections.Generic;

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  /// <summary>Represents detail of a response that an execution of a query returns.</summary>
  /// <typeparam name="TEntity">A type of an entity that it stores in Cosmos DB.</typeparam>
  public sealed class DocumentResponse<TEntity> where TEntity : class
  {
    /// <summary>Gets/sets an object that represents a collection of documets.</summary>
    public IEnumerable<Document<TEntity>> Documents { get; set; }
  }
}
