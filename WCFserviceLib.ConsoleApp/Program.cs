using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCFserviceLib.ConsoleApp
{
    class Program
    {
        // To host this service you need to run VS as administrator
        static void Main(string[] args)
        {
            Uri httpUrl = new Uri("http://localhost:8080/WCFServiceLib/WCFservice");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(WCFservice), httpUrl);
            WSHttpBinding binding = new WSHttpBinding();

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IWCFservice), binding, httpUrl);

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();
            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press  key to stop");
            Console.ReadLine();
        }
    }
}
