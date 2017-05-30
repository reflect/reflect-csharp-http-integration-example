using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reflect.Integration.API
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttributeType
    {
        [EnumMember(Value = "number")]
        NUMBER,
        [EnumMember(Value = "text")]
        TEXT,
        [EnumMember(Value = "date")]
        DATE
    }
}
