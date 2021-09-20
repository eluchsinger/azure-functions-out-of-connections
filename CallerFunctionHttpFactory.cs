using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FunctionConnectionExhaustion
{
    public static class CallerFunctionHttpFactory
    {
        [Function("CallerFunctionHttpFactory")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            const int amountOfCalls = 1250;

            var connector = executionContext.InstanceServices.GetRequiredService<Connector>();
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