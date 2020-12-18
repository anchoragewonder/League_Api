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
using League_Api.RequestModels;

namespace League_Api.Functions
{
    public class QuizRequestFunction
    {
        public async Task<APIGatewayProxyResponse> Execute(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            
            QuizRequestModel jsonRequest;

            jsonRequest = JsonConvert.DeserializeObject<QuizRequestModel>(apigProxyEvent.Body);
            
            try
            {
                QuizChampResponseModel jsonResponse = await GetQuizChamp(jsonRequest);

                return new APIGatewayProxyResponse
                {
                    Body = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, { "Access-Control-Allow-Origin", "*" } }
                };
            }

            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    Body = $"Not valid quiz inputs please try again Error {ex.Message}",
                    StatusCode = 403,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, { "Access-Control-Allow-Origin", "*" } }
                };
            }
        }

        public async Task<QuizChampResponseModel> GetQuizChamp(QuizRequestModel request)
        {
            TableInterface table = new TableInterface();
            List<ChampModel> list = await table.QuizChamp(request);
            QuizChampResponseModel response = new QuizChampResponseModel(list);
            return response;
        }
    }
}
