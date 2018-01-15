using Newtonsoft.Json;
using NLog;
using StackOverflowClient.Common;
using System;
using System.IO;

namespace WCFserviceLib
{
    public class WCFservice : IWCFservice
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Response MakeRequest(string parameter)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "sample.json";

            using (StreamReader r = new StreamReader(path))
            {
                try
                {
                    string responseString = r.ReadToEnd();
                    var responseObject = JsonConvert.DeserializeObject<Response>(responseString);
                    return responseObject;
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                    return new Response();
                }
            }

        }
    }
}
