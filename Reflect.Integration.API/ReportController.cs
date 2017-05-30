using System;
using System.IO;
using System.Web.Http;
using Newtonsoft.Json;

namespace Reflect.Integration.API
{
    public class ReportController : ApiController
    {
        public Report Post() {
            string content = Request.Content.ReadAsStringAsync().Result;
            var settings = JsonConvert.DeserializeObject<ReportSettings>(content);
            return GenerateReport(settings);
        }

        private Report GenerateReport(ReportSettings settings) {
            Statement statement = Statement.FromReportSettings(settings);
            Console.Out.Write("Statement: " + statement.ToSql());

            PostgresReportRunner runner = new PostgresReportRunner();
            return runner.Execute(settings, statement);
        }
    }
}
