using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace League_Api.ResponseModels
{
    public class GetChampionModel
    {

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "class", Order = 2)]
        public string Class { get; set; }

        [JsonProperty(PropertyName = "damage", Order = 3)]
        public int Damage { get; set; }

        [JsonProperty(PropertyName = "damageType", Order = 4)]
        public string DamageType { get; set; }

        [JsonProperty(PropertyName = "defense", Order = 5)]
        public int Sturdiness { get; set; }

        [JsonProperty(PropertyName = "mobility", Order = 6)]
        public int Mobility { get; set; }

        [JsonProperty(PropertyName = "style", Order = 7)]
        public int Style { get; set; }

        [JsonProperty(PropertyName = "crowdControl", Order = 8)]
        public int CrowdControl { get; set; }

        [JsonProperty(PropertyName = "difficulty", Order = 9)]
        public int Difficulty { get; set; }

        [JsonProperty(PropertyName = "factor", Order = 10, NullValueHandling = NullValueHandling.Ignore)]
        public int? Factor { get; set; }

        public GetChampionModel() { }

        public GetChampionModel(TableModels.ChampModel model)
        {
            this.Name = model.Name;
            this.Class = model.Class;
            this.Damage = model.Damage;
            this.DamageType = model.DamageType;
            this.Sturdiness = model.Sturdiness;
            this.Mobility = model.Mobility;
            this.Style = model.Style;
            this.CrowdControl = model.CrowdControl;
            this.Difficulty = model.Difficulty;
            this.Factor = model.Factor;
        }

    }
}
