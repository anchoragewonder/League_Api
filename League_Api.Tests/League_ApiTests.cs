using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using League_Api;
using League_Api.Extensions;
using League_Api.DbSchema;
using League_Api.TableModels;
using League_Api.Functions;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace League_Api.Tests
{
    public class League_ApiTests
    {
        public League_ApiTests()
        {
        }

        [Fact]
        public async void TestDBConnection()
        {
            DbConnector connector = new DbConnector();
            bool is_connected = await connector.IsConnected();
            Assert.True(is_connected);
            await connector.Disconnect();
        }

        [Fact]
        public async Task TestDBGet()
        {
            TableInterface table = new TableInterface();
            ChampModel champ = await table.GetChamp("ziggs");
            Assert.NotNull(champ);
        }

        [Fact]
        public async Task TestGetChampFunction()
        {
            GetChampionFunction func = new GetChampionFunction();
            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.PathParameters = new Dictionary<string, string>();
            request.PathParameters.Add("name", "ziggs");
            TestLambdaContext testContext = new TestLambdaContext();

            APIGatewayProxyResponse response = await func.Execute(request, testContext);
            Assert.NotNull(response.Body);
            Assert.Equal(200, response.StatusCode);

            request = new APIGatewayProxyRequest();
            request.PathParameters = new Dictionary<string, string>();
            request.PathParameters.Add("name", "ziiigggs");
            response = await func.Execute(request, testContext);
            Assert.Equal(403, response.StatusCode);
        }

        [Fact]

        public async Task TestEmptyFunction()
        {
            GetChampionFunction func = new GetChampionFunction();
            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            TestLambdaContext testContext = new TestLambdaContext();

            APIGatewayProxyResponse response = await func.Execute(request, testContext);
            Assert.NotNull(response.Body);
            Assert.Equal(200, response.StatusCode);            
        }
    }
}
