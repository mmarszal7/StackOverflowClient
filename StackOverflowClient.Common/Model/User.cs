namespace StackOverflowClient.Common
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [JsonProperty("display_name")]
        public string Name { get; set; }

        [JsonProperty("profile_image")]
        public string AvatarURL { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("badge_counts")]
        public virtual BadgeCollection BadgeCollection { get; set; }


        [Key, ForeignKey("Topic")]
        public int TopicID { get; set; }

        public int UserID { get; set; }

        public virtual Topic Topic { get; set; }
    }

    public class BadgeCollection
    {
        [JsonProperty("gold")]
        public int GoldenBadges { get; set; }

        [JsonProperty("silver")]
        public int SilverBadges { get; set; }

        [JsonProperty("bronze")]
        public int BronzeBadges { get; set; }


        [Key, ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
