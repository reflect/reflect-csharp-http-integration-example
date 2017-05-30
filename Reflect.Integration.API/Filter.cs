using System;
namespace Reflect.Integration.API
{
    
    public class Filter
    {
        public string Field { get; set; }
        public FilterOperator Op { get; set; }
        public string Value { get; set; }
    }
}
