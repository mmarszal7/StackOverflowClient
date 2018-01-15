using StackOverflowClient.Common;
using System.ServiceModel;

namespace WCFserviceLib
{
    [ServiceContract]
    public interface IWCFservice
    {
        [OperationContract]
        Response MakeRequest(string parameter);
    }
}