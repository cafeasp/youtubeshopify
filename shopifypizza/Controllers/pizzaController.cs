using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shopifypizza.Controllers
{
    public class pizzaController : ApiController
    {
        public static string Scope = ConfigurationManager.AppSettings["Scope"];
        public static string AppId = ConfigurationManager.AppSettings["AppId"];
        public static string Secret = ConfigurationManager.AppSettings["Secret"];

        [HttpGet]
        public HttpResponseMessage install(string shop, string host = "")
        {
            string r = string.Format("https://{0}/admin/oauth/authorize?client_id={1}&scope={2}&redirect_uri=https://{3}/api/pizza/auth", shop, AppId, Scope, "ec7d4adce8ae.ngrok.io");

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(r);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage auth(string shop, string code, string host)
        {

            var token = GetToken(shop, code);

            string r = "https://google.com";

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(r);
            return response;

        }

        private string GetToken(string shop, string code)
        {

            try
            {
                string accessTokenUrl = string.Format("https://{0}/", shop);

                var client = new RestClient(accessTokenUrl);

                var request = new RestRequest("admin/oauth/access_token", Method.Post);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", "client_id=" + AppId + "&client_secret=" + Secret + "&code=" + code, ParameterType.RequestBody);

                var response = client.Execute(request);

                var r = JsonConvert.DeserializeObject<dynamic>(response.Content);
                var accessToken = r.access_token;
                return (string)accessToken;
            }
            catch (Exception e)
            {

                return string.Empty;
            }


        }
    }
}
