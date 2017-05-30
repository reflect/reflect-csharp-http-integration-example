using System;
using System.Collections.Generic;
using Npgsql;

namespace Reflect.Integration.API
{
    public class PostgresReportRunner : ReportRunner
    {
        public override Report Execute(ReportSettings settings, Statement statement)
        {
            var report = new Report();

            var connString = "Host=localhost";

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            var cmd = new NpgsqlCommand(statement.ToSql(), conn);
            var reader = cmd.ExecuteReader();

            while(reader.Read()) {
                int dimsCount = 0;
                int metsCount = 0;

                var dims = settings.Dimensions.GetEnumerator();
                var finalDims = new List<string>();

                while(dims.MoveNext()) {
                    finalDims.Add(reader.GetString(dimsCount));
                    dimsCount++;
                }

				var mets = settings.Metrics.GetEnumerator();
				var finalMets = new List<double>();

                while(mets.MoveNext()) {
                    finalMets.Add(reader.GetDouble(dimsCount + metsCount));
                    metsCount++;
                }

                report.AddRow(finalDims, finalMets);
			}

            return report;
        }
    }
}
