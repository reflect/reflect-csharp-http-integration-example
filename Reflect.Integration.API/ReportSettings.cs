using System;
using System.Collections.Generic;

namespace Reflect.Integration.API
{
    public class ReportSettings
    {
		public IEnumerable<string> Dimensions { get; set; }
		public IEnumerable<string> Metrics { get; set; }
		public IEnumerable<Filter> Filters { get; set; }
        public SortConfiguration Sort { get; set; }

        public ReportSettings()
        {
            // Just initialize the types so we don't do anything dumb.
            Dimensions = new List<string>();
            Metrics = new List<string>();
            Filters = new List<Filter>();
            Sort = new SortConfiguration();
        }
    }
}
