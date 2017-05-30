using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reflect.Integration.API
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum SortDirection
    {
		[EnumMember(Value = "ascending")]
		ASCENDING,
		[EnumMember(Value = "descending")]
		DESCENDING
    }
}
