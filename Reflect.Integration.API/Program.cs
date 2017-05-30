using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Reflect.Integration.API
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var server = new Server("http://localhost:8080");
            server.Start();
        }
    }
}
