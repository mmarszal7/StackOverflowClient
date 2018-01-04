using StackOverflowClient.Common;
using System.Data.Entity;
using System.Data.SQLite;

namespace StackOverflowClient.SQLiteRepository
{
    public class DataBaseContext : DbContext
    {
        //private const string connectionString = "Data Source=./StackOverflowDB.db;Version=3";
        private const string connectionString = "Data Source=C:\\Users\\mmarszalek\\Documents\\Visual Studio 2017\\Projects\\StackOverflowClient\\StackOverflowDB.db;Version=3";

        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BadgeCollection> BadgeCollections { get; set; }

        public DataBaseContext() : base(new SQLiteConnection() { ConnectionString = connectionString }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BadgeCollection>()
                .HasKey(e => e.UserID);

            modelBuilder.Entity<User>()
                .HasKey(e => e.TopicID)
                .HasRequired(s => s.BadgeCollection)
                .WithRequiredPrincipal(ad => ad.User);

            modelBuilder.Entity<Topic>()
                        .HasRequired(s => s.User)
                        .WithRequiredPrincipal(ad => ad.Topic);
        }
    }
}
