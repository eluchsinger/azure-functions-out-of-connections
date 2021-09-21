using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace FunctionConnectionExhaustion
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddScoped<Connector>();
                    services.AddHttpClient<Connector>()
                        .ConfigurePrimaryHttpMessageHandler(() =>{
                            return new SocketsHttpHandler() {
                                MaxConnectionsPerServer = 100
                            };
                        });
                })
                .Build();

            host.Run();
        }
    }
}