using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using League_Api.TableModels;

using Newtonsoft.Json;

namespace League_Api.ResponseModels
{
    public class QuizChampResponseModel
    {
        [JsonProperty(PropertyName = "Champions")]
        public List<GetChampionModel> Champions;

        public QuizChampResponseModel() { }

        public QuizChampResponseModel(List<ChampModel> list)
        {
            List<GetChampionModel> champions = new List<GetChampionModel>();
            foreach(ChampModel cm in list)
            {
                champions.Add(new GetChampionModel(cm));
            }
            champions = champions.OrderByDescending(x => x.Factor).ToList();
            this.Champions = champions;
        }
    }
}
