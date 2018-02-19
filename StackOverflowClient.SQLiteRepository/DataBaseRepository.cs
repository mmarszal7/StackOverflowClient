namespace StackOverflowClient.SQLiteRepository
{
    using StackOverflowClient.Common;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseRepository : IDataBaseRepository
    {
        public void AddNewTopic(Topic newTopic)
        {
            using (var db = new DataBaseContext())
            {
                db.Topics.Add(newTopic);
                db.SaveChanges();
            };
        }

        public List<Topic> GetTopics(string query)
        {
            List<Topic> response = new List<Topic>();

            using (var db = new DataBaseContext())
            {
                response = db.Topics.Where(p => p.Title.Contains(query.ToLower())).Include(topic => topic.User).Include(topic2 => topic2.User.BadgeCollection).ToList();
            };

            return response;
        }
    }
}
