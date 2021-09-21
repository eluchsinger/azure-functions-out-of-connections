using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

namespace FunctionConnectionExhaustion
{
    public static class CallerFunctionHttpClientFactory
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("CallerFunctionHttpClientFactory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var connector = new Connector(httpClient);

            const int AmountOfRequests = 1250;
            var tasks = new List<Task>(AmountOfRequests);

            for (var i = 0; i < AmountOfRequests; i++)
            {
                var task = connector.DoRequest();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            return new OkObjectResult("Worked!");
        }
    }
}
