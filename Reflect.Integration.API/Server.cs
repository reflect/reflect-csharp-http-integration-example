using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;
            
namespace Reflect.Integration.API
{
    public class Server
    {
        private string _baseAddress;

        public Server(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public void Start()
        {
            var config = new HttpSelfHostConfiguration(this._baseAddress);
            config.Routes.MapHttpRoute(name: "Generate Report", routeTemplate: "report", defaults: new { controller = "Report"});
			config.Routes.MapHttpRoute(name: "List Attributes", routeTemplate: "attributes", defaults: new { controller = "Attributes" });

			using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {
                server.OpenAsync().Wait();
                Console.Out.WriteLine("Press <ENTER> to stop server.");
                Console.In.ReadLine();
            }
        }
    }
}
