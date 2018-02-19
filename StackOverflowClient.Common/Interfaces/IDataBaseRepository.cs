namespace StackOverflowClient.Common
{
    using System.Collections.Generic;

    public interface IDataBaseRepository
    {
        void AddNewTopic(Topic newTopic);
        List<Topic> GetTopics(string query);
    }
}