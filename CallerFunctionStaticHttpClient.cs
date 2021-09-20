using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FunctionConnectionExhaustion
{
    public static class CallerFunctionStaticHttpClient
    {
        private static readonly HttpClient staticHttpClient = new HttpClient();

        [Function("CallerFunctionStaticHttpClient")]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            const int amountOfCalls = 1250;

            var logger = executionContext.InstanceServices.GetRequiredService<ILogger<Connector>>();
            var connector = new Connector(staticHttpClient, logger);
            var tasks = new List<Task>(amountOfCalls);
            for (var i = 0; i < amountOfCalls; i++)
            {
                var task = connector.DoRequest();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            var response = req.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
