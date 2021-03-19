using System.Threading;
using System.Threading.Tasks;

namespace AzureFunctionsCustomBindingSample.Binding.Document
{
  public interface IQueryHandler<TQuery>
  {
    public Task<object> HandleAsync(TQuery query, CancellationToken cancellationToken);
  }
}
