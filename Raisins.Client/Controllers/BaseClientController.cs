using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Controllers
{
    public class BaseClientController : Controller
    {
        public IRestResponse RestResponse { get; protected set; }

        public O CallAPI<I, O>(string url)
            where I : new()
        {
            return CallAPI<I, O>(url, Method.GET);
        }

        public O CallAPI<I, O>(string url, params Parameter[] parameters)
            where I : new()
        {
            return CallAPI<I, O>(url, Method.GET, parameters);
        }


        public O CallAPI<I, O>(string url, Method method, params Parameter[] parameters)
            where I : new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter);
            }

            RestResponse = client.Execute<I>(request);

            return Deserialize<O>(RestResponse);
        }

        protected virtual T Deserialize<T>(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            return Deserialize<T>(response, deserializer);
        }

        protected virtual T Deserialize<T>(IRestResponse response, JsonDeserializer deserializer)
        {
            return deserializer.Deserialize<T>(response);
        }
    }
}