﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EANInterface.JsonNET.EANHotelListJsonTypes;

namespace EANInterface.JsonNET.EANHotelListJsonTypes
{

    internal class NightlyRate
    {

        [JsonProperty("@baseRate")]
        public string BaseRate { get; set; }

        [JsonProperty("@rate")]
        public string Rate { get; set; }

        [JsonProperty("@promo")]
        public string Promo { get; set; }
    }

}
