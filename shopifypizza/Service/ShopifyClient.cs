using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using shopifypizza.Models;

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
        public int GetProductCount()
        {

            try
            {
                string productCount = string.Format("https://{0}/", Shop);

                client = new RestClient(productCount);

                request = new RestRequest("admin/api/2023-01/products/count.json", Method.Get);
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
        public ProductListModel GetProducts() {

            try
            {
                string productUrl = string.Format("https://{0}/", Shop);

                client = new RestClient(productUrl);

                request = new RestRequest("admin/api/2023-01/products.json", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("X-Shopify-Access-Token", Token);

                var response = client.Execute(request);
                
                var r = JsonConvert.DeserializeObject<dynamic>(response.Content);

                var products = new ProductListModel();

                foreach (var item in r.products)
                {
                    products.Products.Add(new ProductModel()
                    { Description = item.body_html, Id=item.id, Title = item.title, Vendor = item.vendor });
                }
          
                return products;           
            }
            catch (Exception e)
            {
                return new ProductListModel();
            }      
        }
    }
}