using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClient.Model
{
    class Response
    {
        [JsonProperty("total")]
        public int NumberOfTopics { get; set; }

        [JsonProperty("items")]
        public List<Topic> TopicList { get; set; }
    }
}
