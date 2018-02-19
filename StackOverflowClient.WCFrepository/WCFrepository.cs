namespace StackOverflowClient.WCFserviceRepository
{
    using StackOverflowClient.Common;
    using WCFserviceLib.Client;

    public class WCFrepository : IRestRepository
    {
        public Response MakeRequest(string parameter)
        {
            var response = new Response();
            WCFserviceClient proxy = new WCFserviceClient();
            response = proxy.MakeRequest(parameter);
            return response;
        }
    }
}