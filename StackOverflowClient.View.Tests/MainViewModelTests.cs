using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackOverflowClient.Common;
using Moq;
using System.Linq;

namespace StackOverflowClient.View.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        public IRestRepository GetTestRestRepository()
        {
            var restMock = new Mock<IRestRepository>();
            restMock.Setup(r => r.MakeHttpRequest(It.IsAny<string>())).Returns(It.IsAny<Response>());
            return restMock.Object;
        }

        public IDataBaseRepository GetTestDataBaseRepository()
        {
            var restMock = new Mock<IDataBaseRepository>();
            return restMock.Object;
        }

        public IDialogService<NewQuestionWindow> GetTestDiagloService()
        {
            var restMock = new Mock<IDialogService<NewQuestionWindow>>();
            return restMock.Object;
        }

        [TestMethod]
        public void SetPaginationTest()
        {
            var restRepository = GetTestRestRepository();
            var dataBaseRepository = GetTestDataBaseRepository();
            var dialogService = GetTestDiagloService();

            var vm = new MainViewModel(dataBaseRepository, restRepository, dialogService);
            PrivateObject obj = new PrivateObject(vm);
            obj.Invoke("SetPagination");
            // Cannot change Page property value, because its setter invokes another functions which requires e.g. not null Topics
            obj.SetField("page", 1); 

            Assert.IsNotNull(vm.Pagination);
            Assert.AreEqual(5, vm.Pagination.ToArray().Count());
            CollectionAssert.AreEqual(Enumerable.Range(1, 5).ToArray(), vm.Pagination);
        }
    }
}
