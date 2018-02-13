using NUnit.Framework;
using StackOverflowClient.Common;
using Moq;
using System.Linq;
using MStest = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Newtonsoft.Json;

namespace StackOverflowClient.View.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        #region Mockups

        public Response MakeRequest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "sample.json");

            using (StreamReader r = new StreamReader(path))
            {
                string responseString = r.ReadToEnd();
                var responseObject = JsonConvert.DeserializeObject<Response>(responseString);
                return responseObject;
            }
        }

        public IRestRepository GetTestRestRepository()
        {
            var restMock = new Mock<IRestRepository>();
            restMock.Setup(r => r.MakeRequest(It.IsAny<string>())).Returns(MakeRequest());
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

        public MainViewModel GetViewModel()
        {
            var restRepository = GetTestRestRepository();
            var dataBaseRepository = GetTestDataBaseRepository();
            var dialogService = GetTestDiagloService();

            return new MainViewModel(dataBaseRepository, restRepository, dialogService);
        }

        #endregion

        [Test]
        public void SearchForTopics()
        {
            var vm = GetViewModel();
            vm.Search.Execute(null);

            Assert.AreEqual(5, vm.Topics.Count);
        }

        [Test]
        public void SetPaginationTest()
        {
            var vm = GetViewModel();
            vm.Search.Execute(null);
            vm.PaginationCommand.Execute("5");

            CollectionAssert.AreEqual(Enumerable.Range(3, 8).ToArray(), vm.Pagination);
        }
    }
}
