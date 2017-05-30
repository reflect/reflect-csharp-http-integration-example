using System.Collections.Generic;
using Newtonsoft.Json;

namespace Reflect.Integration.API
{
    public class Report
    {
        [JsonProperty(PropertyName = "results")]
        public IList<ReportResultRow> Results { get; set; }

        public Report() {
            Results = new List<ReportResultRow>();
        }

        public void AddRow(IEnumerable<string> dims, IEnumerable<double> mets) {
            var row = new ReportResultRow();
            row.Dimensions = dims;
            row.Metrics = mets;

            Results.Add(row);
        }

        public class ReportResultRow {
            [JsonProperty(PropertyName = "dimensions")]
            public IEnumerable<string> Dimensions { get; set; }

            [JsonProperty(PropertyName = "metrics")]
            public IEnumerable<double> Metrics { get; set; }
        }
    }
}
