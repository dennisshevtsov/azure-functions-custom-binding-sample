using AzureFunctionsCustomBindingSample.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureFunctionsCustomBindingSample.Api.Tests
{
  [TestClass]
  public sealed class TodoServiceTest
  {
    private ITodoService _todoService;

    [TestInitialize]
    public void Initialize()
    {
      _todoService = new TodoService();
    }

    [TestMethod]
    public void TestMethod1()
    {
    }
  }
}
