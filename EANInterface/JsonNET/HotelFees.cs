﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EANInterface.JsonNET.EANHotelListJsonTypes;

namespace EANInterface.JsonNET.EANHotelListJsonTypes
{

    internal class HotelFees
    {

        [JsonProperty("@size")]
        public string Size { get; set; }

        [JsonProperty("HotelFee")]
        [JsonConverter(typeof(SingleValueArrayConverter<HotelFee>))]
        public HotelFee[] HotelFee { get; set; }
    }

}
