using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace League_Api.RequestModels
{
    public class QuizRequestModel
    {
        [JsonProperty(PropertyName = "damage", Order = 1)]
        public int Damage { get; set; }

        [JsonProperty(PropertyName = "defense", Order = 2)]
        public int Sturdiness { get; set; }

        [JsonProperty(PropertyName = "mobility", Order = 3)]
        public int Mobility { get; set; }

        [JsonProperty(PropertyName = "crowdControl", Order = 4)]
        public int CrowdControl { get; set; }

        public QuizRequestModel() { }

        public QuizRequestModel(int damage, int defense, int mobility, int crowdControl)
        {
            this.Damage = damage;
            this.Sturdiness = defense;
            this.Mobility = mobility;
            this.CrowdControl = crowdControl; 
        }
    }

}
