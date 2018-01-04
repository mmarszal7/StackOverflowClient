using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StackOverflowClient.Common
{
    public class Topic
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Content { get; set; }

        [JsonProperty("up_vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("answer_count")]
        public int AnswerCount { get; set; }

        [JsonProperty("view_count")]
        public long ViewCount { get; set; }

        [JsonProperty("creation_date")]
        public long CreationDate { get; set; }

        [JsonProperty("owner")]
        public virtual User User { get; set; }

        [JsonProperty("tags")]
        [NotMapped]
        public List<string> Tags { get; set; }
        private List<string> tags;

        [Column("Tags")]
        public string StringTags { get; set; }


        public int TopicID { get; set; }
    }
}
