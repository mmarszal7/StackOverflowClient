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
        private readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "sample.json");

        public Response MakeRequest(string parameter)
        {
            using (StreamReader r = new StreamReader(path))
            {
                try
                {
                    string responseString = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Response>(responseString);
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
