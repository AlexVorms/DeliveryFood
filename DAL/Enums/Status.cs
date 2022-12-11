﻿using System.Text.Json.Serialization;

namespace WebApplication2.DAL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        InProcess,
        Delivered

    }
}
