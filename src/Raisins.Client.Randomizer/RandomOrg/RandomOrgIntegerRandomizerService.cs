using Newtonsoft.Json;
using Raisins.Client.Randomizer.Interfaces;
using Raisins.Client.Randomizer.RandomOrg.Request;
using Raisins.Client.Randomizer.RandomOrg.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.Randomizer.RandomOrg
{
    public class RandomOrgIntegerRandomizerService
    {
        public const string RandomOrgUri = "https://api.random.org/json-rpc/1/invoke";
        
        public async Task<int> GetNext(int min, int max, int iterations)
        {
            max = 10;

            var integerRequest = CreateIntegerRequest(min, max, iterations);

            var requestUri = new Uri(RandomOrgUri);

            GenerateIntegerResponse response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = requestUri;

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
                httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpRequest.Content = new StringContent(
                                        JsonConvert.SerializeObject(integerRequest),
                                        Encoding.UTF8);

                var httpResponse = await client.PostAsync(requestUri, httpRequest.Content);

                httpResponse.EnsureSuccessStatusCode();

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<GenerateIntegerResponse>(jsonResponse);
            }

            if(response == null)
            {
                throw new InvalidOperationException("Response was null.");
            }

            //todo: this looks terrible, but the request spec has to be followed
            return response.result.random.data.First();
        }

        private GenerateIntegerRequest CreateIntegerRequest(int min, int max, int iterations)
        {
            return new GenerateIntegerRequest()
            {
                id = 4242,
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new GenerateIntegerRequestParams()
                {
                    apiKey = "",
                    @base = 10,
                    min = min,
                    max = max,
                    n = 1,
                    replacement = false
                }
            };
        }
    }
}
