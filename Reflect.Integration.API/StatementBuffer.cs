using System;
using System.Collections.Generic;
using System.Text;

namespace Reflect.Integration.API
{
    public class StatementBuffer
    {
        private StringBuilder _buffer;

        public StatementBuffer()
        {
            _buffer = new StringBuilder("SELECT ");
        }

        public new string ToString()
        {
            return _buffer.ToString();
        }

        public void AppendFields(IList<string> fields) {
            if (fields.Count > 0)
            {
                _buffer.Append(String.Join(", ", fields));
                _buffer.Append(" ");
            }
        }

        public void AppendTable(string table) {
            _buffer.Append("FROM ");
            _buffer.Append(table);
            _buffer.Append(" ");
        }

        public void AppendConditions(IList<string> conditions) {
            if (conditions.Count > 0)
            {
                _buffer.Append("WHERE ");
                _buffer.Append(String.Join(" AND ", conditions));
                _buffer.Append(" ");
            }
        }

        public void AppendOrder(string order, string direction) {
            if (!String.IsNullOrWhiteSpace(order)) {
                _buffer.Append("ORDER BY ");
                _buffer.Append(order);
                _buffer.Append(" ");
                _buffer.Append(direction);
                _buffer.Append(" ");
            }
        }

        public void AppendGroups(IList<string> groups) {
            if (groups.Count > 0) {
                _buffer.Append("GROUP BY ");
                _buffer.Append(String.Join(", ", groups));
                _buffer.Append(" ");
            }
        }
    }
}
