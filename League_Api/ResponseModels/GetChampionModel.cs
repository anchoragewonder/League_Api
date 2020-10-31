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
    }
}
