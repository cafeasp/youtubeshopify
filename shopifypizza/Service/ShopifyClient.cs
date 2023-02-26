using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
namespace shopifypizza.Service
{
    public class ShopifyClient
    {
        private RestClient client;
        private RestRequest request;
        public string Shop { get; set; }
        public string Token { get; set; }

        public ShopifyClient(string shop,string token)
        {
            Shop = shop;
            Token = token;
        }
        public int GetProductCount() {

            try
            {
                string productCount = string.Format("https://{0}/", Shop);

                var client = new RestClient(productCount);

                var request = new RestRequest("admin/api/2023-01/products/count.json", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("X-Shopify-Access-Token", Token);

                var response = client.Execute(request);
                //{"count":0}
                var r = JsonConvert.DeserializeObject<dynamic>(response.Content);
                var count = r.count;
                return (int)count;

              
            }
            catch (Exception e)
            {

                return 0;
            }

           
        
        
        }
    }
}