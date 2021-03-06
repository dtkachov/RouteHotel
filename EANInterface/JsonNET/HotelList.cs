﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EANInterface.JsonNET.EANHotelListJsonTypes;

namespace EANInterface.JsonNET.EANHotelListJsonTypes
{

    internal class HotelList
    {

        [JsonProperty("@size")]
        public string Size { get; set; }

        [JsonProperty("@activePropertyCount")]
        public string ActivePropertyCount { get; set; }

        [JsonProperty("HotelSummary")]
        [JsonConverter(typeof(SingleValueArrayConverter<HotelSummary>))]
        public HotelSummary[] HotelSummary { get; set; }
    }

}
