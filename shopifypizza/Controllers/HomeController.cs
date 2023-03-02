using shopifypizza.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopifypizza.Models;
using System.Collections.Specialized;

namespace shopifypizza.Controllers
{
    public class HomeController : Controller
    {
        private string Token = ConfigurationManager.AppSettings["Token"];
        public ActionResult Index()
        {
            var ShopifyProduct = new ShopifyClient("pizza-for-youtube.myshopify.com", Token);
            var count = ShopifyProduct.GetProductCount();
            var products = ShopifyProduct.GetProducts();
            products.Count = count;
            
            return View(products);
        }

        
    }
}