using System;
using System.Collections.Generic;

namespace Reflect.Integration.API
{
    public class AttributesMap : Dictionary<string, AttributeType>
    {
        /// <summary>
        /// This dictionary will contain any mapping between expressions and the
        /// attributes we want to expose. This is really only relevant for metrics,
        /// where there will be an expression to calculate the metric.
        /// </summary>
        private Dictionary<string, string> _expressionOverrides;

        /// <summary>
        /// This dictionary will contain a mapping between columns and the
        /// attributes we want to expose.
        /// </summary>
        private Dictionary<string, string> _columnOverrides;

        public AttributesMap()
        {
			Add("RowId", AttributeType.NUMBER);
			Add("OrderId", AttributeType.NUMBER);
			Add("OrderDate", AttributeType.DATE);
			Add("OrderPriority", AttributeType.TEXT);
			Add("OrderQuantity", AttributeType.NUMBER);
			Add("AvgSales", AttributeType.NUMBER);
			Add("SumSales", AttributeType.NUMBER);
			Add("SumDiscount", AttributeType.NUMBER);
			Add("ShipMode", AttributeType.TEXT);
			Add("SumProfit", AttributeType.NUMBER);
			Add("SumUnitPrice", AttributeType.NUMBER);
			Add("SumShippingCost", AttributeType.NUMBER);
			Add("CustomerName", AttributeType.TEXT);
			Add("City", AttributeType.TEXT);
			Add("ZipCode", AttributeType.NUMBER);
			Add("State", AttributeType.TEXT);
			Add("Region", AttributeType.TEXT);
			Add("CustomerSegment", AttributeType.TEXT);
			Add("ProductCategory", AttributeType.TEXT);
			Add("ProductSubCategory", AttributeType.TEXT);
			Add("ProductName", AttributeType.TEXT);
			Add("ProductContainer", AttributeType.TEXT);
			Add("AvgProductBaseMargin", AttributeType.NUMBER);
			Add("ShipDate", AttributeType.DATE);

            // Populate any expressions here.
            _expressionOverrides = new Dictionary<string, string>();
            _expressionOverrides.Add("ShipDate", "DATE_TRUNC('day', ship_date)");
            _expressionOverrides.Add("AvgSales", "AVG(sales)");
            _expressionOverrides.Add("SumSales", "SUM(sales)");
            _expressionOverrides.Add("SumDiscount", "SUM(discount)");
            _expressionOverrides.Add("SumProfit", "SUM(profit)");
            _expressionOverrides.Add("SumUnitPrice", "SUM(unit_price)");
            _expressionOverrides.Add("SumShippingCost", "SUM(shipping_cost)");
            _expressionOverrides.Add("AvgProductBaseMargin", "AVG(base_margin)");

            _columnOverrides = new Dictionary<string, string>();
			_columnOverrides.Add("AvgSales", "sales");
			_columnOverrides.Add("SumSales", "sales");
			_columnOverrides.Add("SumDiscount", "discount");
			_columnOverrides.Add("SumProfit", "profit");
			_columnOverrides.Add("SumUnitPrice", "unit_price");
			_columnOverrides.Add("SumShippingCost", "shipping_cose");
			_columnOverrides.Add("AvgProductBaseMargin", "base_margin");
			_columnOverrides.Add("RowId", "row_id");
			_columnOverrides.Add("OrderId", "order_id");
			_columnOverrides.Add("OrderDate", "order_date");
			_columnOverrides.Add("OrderPriority", "order_priority");
			_columnOverrides.Add("OrderQuantity", "order_quantity");
			_columnOverrides.Add("ShipMode", "ship_mode");
			_columnOverrides.Add("CustomerName", "customer_name");
			_columnOverrides.Add("City", "city");
			_columnOverrides.Add("ZipCode", "zip_code");
			_columnOverrides.Add("State", "state");
			_columnOverrides.Add("Region", "region");
			_columnOverrides.Add("CustomerSegment", "customer_segment");
			_columnOverrides.Add("ProductCategory", "product_category");
			_columnOverrides.Add("ProductSubCategory", "product_sub_category");
			_columnOverrides.Add("ProductName", "product_name");
			_columnOverrides.Add("ProductContainer", "product_container");
			_columnOverrides.Add("ShipDate", "ship_date");
        }

        public string GetExpressionFromAttribute(string attribute) {
			if (!ContainsKey(attribute))
			{
				throw new InvalidStatementException("attribute " + attribute + " is not supported.");
			}

			// If we've overridden the column name, we'll use that here.
			string expression;

            if (_expressionOverrides.TryGetValue(attribute, out expression))
			{
				return expression;
			}

            // If there's no expression, we'll see if there's a column to use.
            // This is typically used for dimensions.
            if (_columnOverrides.TryGetValue(attribute, out expression)) {
                return expression;
            }

			// We didn't have an expression override so we just assume the attribute
			// name maps to the expression.
			return attribute;
        }

        public string GetColumnFromAttribute(string attribute) {
            if (!ContainsKey(attribute)) {
                throw new InvalidStatementException("attribute " + attribute + " is not supported.");
            }

            // If we've overridden the column name, we'll use that here.
            string column;

            if (_columnOverrides.TryGetValue(attribute, out column)) {
                return column;
            }

            // We didn't have a column override so we just assume the attribute
            // name maps to the column.
            return attribute;
        }
    }
}
