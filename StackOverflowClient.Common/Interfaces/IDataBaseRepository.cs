using System.Collections.Generic;

namespace StackOverflowClient.Common
{
    public interface IDataBaseRepository
    {
        void AddNewTopic(Topic newTopic);
        List<Topic> GetTopics(string query);
    }
}