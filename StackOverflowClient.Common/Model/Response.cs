namespace StackOverflowClient.Common
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Response
    {
        [JsonProperty("total")]
        public int NumberOfTopics { get; set; }

        [JsonProperty("items")]
        public List<Topic> TopicList { get; set; }
    }
}
