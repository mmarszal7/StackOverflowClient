namespace StackOverflowClient.WCFserviceRepository
{
    using StackOverflowClient.Common;

    public class WCFrepository : IRestRepository
    {
        public Response MakeRequest(string parameter)
        {
            var response = new Response();
            //IRestRepository proxy = new WCFserviceClient();
            //response = proxy.MakeRequest(parameter);
            return response;
        }
    }
}