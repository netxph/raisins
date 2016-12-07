﻿using Raisins.Client.Randomizer.RandomOrg.Request;
using Raisins.Client.Randomizer.RandomOrg.Response;
using RestSharp;
using System;
using System.Linq;

namespace Raisins.Client.Randomizer.RandomOrg
{
    public class RandomOrgIntegerRandomizerService
    {
        private readonly string _apiKey;

        protected string ApiKey
        {
            get
            {
                return _apiKey;
            }
        }

        public const string RandomOrgUri = "https://api.random.org/json-rpc/1/invoke";

        //todo: add throttling

        public RandomOrgIntegerRandomizerService(string apiKey)
        {
            if(String.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }

            _apiKey = apiKey;
        }

        public int GetNext(int min, int max, int iterations)
        {
            RestClient client = new RestClient(RandomOrgUri);

            var request = new RestRequest(Method.POST);

            request.AddJsonBody(CreateIntegerRequest(min, max, iterations));

            var response = client.Execute<GenerateIntegerResponse>(request).Data;
            
            if(response.Error != null)
            {
                throw new InvalidOperationException(response.Error.Message);
            }

            return response.Result.Random.Data.First();
        }

        protected virtual GenerateIntegerRequest CreateIntegerRequest(int min, int max, int iterations)
        {
            return new GenerateIntegerRequest()
            {
                id = 4242,
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new GenerateIntegerRequestParams()
                {
                    apiKey = ApiKey,
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
