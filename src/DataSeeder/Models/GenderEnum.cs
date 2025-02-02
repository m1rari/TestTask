using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DataSeeder.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenderEnum : byte
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,

    [EnumMember(Value = "male")]
    Male = 1,

    [EnumMember(Value = "female")]
    Female = 2,

    [EnumMember(Value = "other")]
    Other = 3
}