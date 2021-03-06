
namespace AzureFunctionsCustomBindingSample.Api.Services
{
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsCustomBindingSample.Api.Dtos;
  using AzureFunctionsCustomBindingSample.Documents;

  public interface ITodoListService
  {
    public Task<CreateTodoListRequestDto> CreateTodoListAsync(
      CreateTodoListRequestDto requestDto,
      UserDocument userDocument,
      CancellationToken cancellationToken);
  }
}
