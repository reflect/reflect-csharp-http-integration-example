using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reflect.Integration.API
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FilterOperator
    {
        [EnumMember(Value = "=")]
        EQUAL,
        [EnumMember(Value = "!=")]
        NOT_EQUAL,
        [EnumMember(Value = "<")]
        LESS_THAN,
        [EnumMember(Value = "<=")]
        LESS_THAN_OR_EQUAL,
        [EnumMember(Value = ">")]
        GREATER_THAN,
        [EnumMember(Value = ">=")]
        GREATER_THAN_OR_EQUAL,
        [EnumMember(Value = "=~")]
        CONTAINS
    }
}
