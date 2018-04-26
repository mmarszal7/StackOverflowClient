namespace StackOverflowClient.View.Tests
{
    using NUnit.Framework;
    using StackOverflowClient.Common;
    using Moq;
    using System.Linq;
    using MStest = Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using StackOverflowClient.ViewModel;

    [TestFixture]
    public class MainViewModelTests
    {
        private Mock<IDataBaseRepository> DbMock = new Mock<IDataBaseRepository>();
        private Mock<IRestRepository> RestMock = new Mock<IRestRepository>();
        private Mock<NewQuestionWindow> NewQuestionWindowMock = new Mock<NewQuestionWindow>();

        public MainViewModelTests()
        {
            RestMock.Setup(r => r.MakeRequest(It.IsAny<string>())).Returns(MakeRequest());
        }

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

        [Test]
        public void SearchForTopics()
        {
            var vm = new MainViewModel(DbMock.Object, RestMock.Object, NewQuestionWindowMock.Object);
            vm.Search.Execute(null);

            Assert.AreEqual(5, vm.Topics.Count);
        }

        [Test]
        public void SetPaginationTest()
        {
            var vm = new MainViewModel(DbMock.Object, RestMock.Object, NewQuestionWindowMock.Object);
            vm.Search.Execute(null);
            vm.PaginationCommand.Execute("5");

            CollectionAssert.AreEqual(Enumerable.Range(3, 8).ToArray(), vm.Pagination);
        }
    }
}
