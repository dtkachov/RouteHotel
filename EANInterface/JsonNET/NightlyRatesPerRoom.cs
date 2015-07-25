﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EANInterface.JsonNET.EANHotelListJsonTypes;

namespace EANInterface.JsonNET.EANHotelListJsonTypes
{

    internal class NightlyRatesPerRoom
    {

        [JsonProperty("@size")]
        public string Size { get; set; }

        [JsonProperty("NightlyRate")]
        [JsonConverter(typeof(SingleValueArrayConverter<NightlyRate>))]
        public NightlyRate[] NightlyRate { get; set; }
    }

}
