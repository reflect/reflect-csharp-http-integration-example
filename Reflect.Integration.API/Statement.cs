using System;
using System.Collections.Generic;

namespace Reflect.Integration.API
{
    public class Statement
    {
        public IList<string> Columns { get; set; }
        public IList<string> Conditions { get; set; }
        public IList<string> Groups { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }

        private Statement()
        {
            Columns = new List<string>();
            Conditions = new List<string>();
            Groups = new List<string>();

            // Default direction to sort by.
            OrderDirection = "DESC";
        }


        public string ToSql() {
            StatementBuffer buf = new StatementBuffer();
            buf.AppendFields(Columns);
            buf.AppendTable("order_data");
            buf.AppendConditions(Conditions);
            buf.AppendGroups(Groups);
            buf.AppendOrder(OrderBy, OrderDirection);
            return buf.ToString();
        }

        private static string FilterOperatorToSqlOperator(FilterOperator op) {
            switch(op) {
                case FilterOperator.EQUAL:
                    return "=";
                case FilterOperator.NOT_EQUAL:
                    return "<>";
                case FilterOperator.GREATER_THAN:
                    return ">";
                case FilterOperator.GREATER_THAN_OR_EQUAL:
                    return ">=";
                case FilterOperator.LESS_THAN:
                    return "<";
                case FilterOperator.LESS_THAN_OR_EQUAL:
                    return "<=";
                case FilterOperator.CONTAINS:
                    return "LIKE";
            }

            throw new InvalidStatementException("Unsupported filter operator: " + op);
        }

        private static string SortDirectionToSqlDirection(SortConfiguration config) {
            switch (config.Direction) {
                case SortDirection.ASCENDING:
                    return "ASC";
                case SortDirection.DESCENDING:
                    return "DESC";
            }

            // Default sort direction.
            return "DESC";
        }

        public static Statement FromReportSettings(ReportSettings settings) {
            Statement statement = new Statement();

            var attributesMap = new AttributesMap();

            int groupIndex = 1;

            foreach (string dim in settings.Dimensions)  {
                statement.Columns.Add(attributesMap.GetExpressionFromAttribute(dim) + " AS " + dim);

                // We always group by dimensions, so we'll just blindly append
                // groups on to the list.
                statement.Groups.Add(groupIndex.ToString());
                groupIndex++;
            }

            foreach (string met in settings.Metrics) {
                statement.Columns.Add(attributesMap.GetExpressionFromAttribute(met) + " AS " + met);
            }

            foreach (Filter filter in settings.Filters) {
                var column = attributesMap.GetColumnFromAttribute(filter.Field);
                var op = FilterOperatorToSqlOperator(filter.Op);
                var val = filter.Value;

                // Special case for CONTAINS operators: we need to enclose in
                // wildcards.
                if (filter.Op == FilterOperator.CONTAINS) {
                    val = "%" + val + "%";
                }

                statement.Conditions.Add(String.Format("{0} {1} '{2}'", column, op, val));
            }

            if (!String.IsNullOrEmpty(settings.Sort.Field)) {
                statement.OrderBy = settings.Sort.Field;
                statement.OrderDirection = SortDirectionToSqlDirection(settings.Sort);
            }

            return statement;
        }
    }
}
