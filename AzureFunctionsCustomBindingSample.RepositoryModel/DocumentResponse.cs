using System.Collections.Generic;

namespace AzureFunctionsCustomBindingSample.RepositoryModel
{
  public sealed class DocumentResponse<TEntity> where TEntity : class
  {
    public IEnumerable<Document<TEntity>> Documents { get; set; }
  }
}
