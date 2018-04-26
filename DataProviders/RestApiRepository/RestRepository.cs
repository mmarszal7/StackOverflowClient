namespace StackOverflowClient.RestApiRepository
{
    using Newtonsoft.Json;
    using StackOverflowClient.Common;
    using System.Net;
    using System.Net.Http;

    public class RestRepository : IRestRepository
    {
        private string uri = "https://api.stackexchange.com/2.2/search?";
        private string filter = $"!-xzyIyy1oK74gRa3b(L6q(T6M.DftxDaDi5IEIU.-";
        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        public Response MakeRequest(string parameter)
        {
            using (HttpClient client = new HttpClient(handler, false))
            {
                var responseString = client.GetStringAsync(uri+ parameter + filter).Result;
                var responseObject = JsonConvert.DeserializeObject<Response>(responseString);
                return responseObject;
            }
        }
    }
}
