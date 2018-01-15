using StackOverflowClient.Common;
using WCFserviceLib.Client;

namespace StackOverflowClient.WCFserviceRepository
{
    public class WCFrepository : IRestRepository
    {
        public Response MakeRequest(string parameter)
        {
            WCFserviceClient proxy = new WCFserviceClient();
            return proxy.MakeRequest(parameter);
        }
    }
}