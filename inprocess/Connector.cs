using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FunctionConnectionExhaustion
{
    public class Connector
    {
        private const string TargetUrl =
            "https://www.apirequest.io/61470119f9f5333af3e42117?at=eyJhcHAiOiI2MTQ3MDExOWY5ZjUzMzNhZjNlNDIxMTciLCJhdWQiOiI2dVowbk9qZnJyOE5JajRyOEk2Tk51clN2RjdWWTJtTCIsInZlciI6IjEiLCJvcmciOiIwOjAiLCJwZXJtaXNzaW9ucyI6eyIwOjAiOnsic2NwIjoiY3JlYXRlOndvcmtzcGFjZXMgcmVhZDp3b3Jrc3BhY2VzIHVwZGF0ZTp3b3Jrc3BhY2VzIGRlbGV0ZTp3b3Jrc3BhY2VzIn19LCJleHAiOjE2MzQ2MDE2MDAsImp0aSI6Ijk4ODQwZWIyLTI2MzYtNGZkZC1iNTkyLThkMWIwMDFiMzhlZSJ9.g9fm2WKyd5YSY6Kg9EL6zn4Ym2G42EpkplojRLsonMU";

        private readonly HttpClient httpClient;

        public Connector(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> DoRequest()
        {
            var response = await httpClient.PostAsync(TargetUrl, new StringContent("Hello World!"));

            return response;
        }
    }
}