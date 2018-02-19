using System.ServiceModel;
using StackOverflowClient.Common;

namespace WCFserviceLib.Client
{
    public class WCFserviceClient : ClientBase<IWCFservice>, IWCFservice
    {
        public Response MakeRequest(string parameter)
        {
            return base.Channel.MakeRequest(parameter);
        }
    }
}
