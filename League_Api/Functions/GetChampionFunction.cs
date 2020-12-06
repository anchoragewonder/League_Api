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

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace League_Api.Functions
{
    public class GetChampionFunction
    {
        private const string EXAMPLE_TEXT = "Here is an example of how to use my api. " +
            "Enter a champion's name and you will get a response as seen below. HAHAHA trolling with teemo. " +
            "Made with <3.";

        public GetChampionFunction()
        {
        }

        public async Task<APIGatewayProxyResponse> Execute(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            if (apigProxyEvent.PathParameters == null)
            {
                GetChampionModel example = await GetResponse("teemo");
                return new APIGatewayProxyResponse
                {
                    Body = $"{EXAMPLE_TEXT}\n{JsonConvert.SerializeObject(example, Formatting.Indented)}",
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            _ = apigProxyEvent.PathParameters.TryGetValue("name", out string name);

            try
            {
                GetChampionModel response = await GetResponse(name);

                return new APIGatewayProxyResponse
                {
                    Body = JsonConvert.SerializeObject(response, Formatting.Indented),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, {"Access-Control-Allow-Origin", "*" } }
                };
            }
            catch(Exception)
            {
                return new APIGatewayProxyResponse
                {
                    Body = $"No champion found with the name: {name}",
                    StatusCode = 403,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
        }

        public async Task<GetChampionModel> GetResponse(string name)
        {
            TableInterface table = new TableInterface();
            ChampModel tableModel = await table.GetChamp(name);
            GetChampionModel response = new GetChampionModel(tableModel);
            return response;
        }
    }
    
}
