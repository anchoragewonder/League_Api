using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace League_Api.ResponseModels
{
    class GetChampionModel
    {

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "class", Order = 2)]
        public string Class { get; set; }

        [JsonProperty(PropertyName = "damage", Order = 3)]
        public int Damage { get; set; }

        [JsonProperty(PropertyName = "damageType", Order = 4)]
        public string DamageType { get; set; }

        [JsonProperty(PropertyName = "sturdiness", Order = 5)]
        public int Defence { get; set; }

        [JsonProperty(PropertyName = "mobiility", Order = 6)]
        public int Mobility { get; set; }

        [JsonProperty(PropertyName = "style", Order = 7)]
        public int Style { get; set; }

        [JsonProperty(PropertyName = "name", Order = 8)]
        public int CrowdControl { get; set; }

        [JsonProperty(PropertyName = "difficulty", Order = 9)]
        public int CardCost { get; set; }

    }
}
