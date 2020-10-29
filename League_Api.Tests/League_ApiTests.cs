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

namespace League_Api.Tests
{
    public class League_ApiTests
    {
        public League_ApiTests()
        {
        }

        [Fact]
        public void TestFunctionHandlerMethod()
        {
            var function = new Function();
            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();
            
            var response = function.FunctionHandler(request, context);
            
            Assert.Equal(200, response.StatusCode);
            Assert.StartsWith("{\"message\":\"hello world\",\"location\":", response.Body);
        }

        [Fact]
        public async void TestDBConnection()
        {
            DbConnector connector = new DbConnector();
            bool is_connected = await connector.IsConnected();
            Assert.True(is_connected);
            await connector.Disconnect();
        }
    }
}
