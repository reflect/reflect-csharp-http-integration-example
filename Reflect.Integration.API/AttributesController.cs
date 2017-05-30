using System;
using System.Web.Http;

namespace Reflect.Integration.API
{
    public class AttributesController : ApiController
    {
        public AttributesMap Get() {
            return new AttributesMap();
        }
    }
}
