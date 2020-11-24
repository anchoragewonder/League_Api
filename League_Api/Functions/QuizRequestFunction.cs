using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using League_Api.TableModels;
using League_Api.ResponseModels;
using League_Api.DbSchema;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace League_Api.Functions
{
    public class QuizRequestFunction
    {
        private const string EXAMPLE_TEXT = "Here is how to use my api. Make a post request using value pairs for attributes" +
            "of League Champions. For example  Defense: 3 , Mobility: 2. The api will then find a champion that best fits those values." +
            "all integers are between 1-9." +
            "The list of attributes are: Mobility Defense CrowdControl Damage";

        public async Task<APIGatewayProxyResponse> Execute(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            QuizRequestModel jsonRequest;

            if (apigProxyEvent.PathParameters == null)
            {
                return new APIGatewayProxyResponse
                {
                    Body = $"{EXAMPLE_TEXT}",
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            try
            {
                GetChampionModel jsonResponse = await GetQuizChamp(jsonRequest);

                return new APIGatewayProxyResponse
                {
                    Body = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented),
                    StatusCode = 203,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            catch (Exception)
            {
                return new APIGatewayProxyResponse
                {
                    Body = $"Not valid quiz inputs please try again",
                    StatusCode = 403,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

        }

        public async Task<GetChampionModel> GetQuizChamp(QuizRequestModel request)
        {
            TableInterface table = new TableInterface();
            ChampModel tableModel = await table.QuizChamp(request);
            GetChampionModel response = new GetChampionModel(tableModel);
            return response;
        }

    }

}
