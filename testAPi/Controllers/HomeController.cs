using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testAPi.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.ComponentModel;

namespace testAPi.Controllers
{
    public class HomeController : Controller
    {
        string baseUrl = "http://localhost:36582/";
        private List<Product> Productlist = new List<Product>();
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Product> product = new List<Product>();
            using(var productlist=new HttpClient())
            {
                productlist.BaseAddress = new Uri(baseUrl);
                productlist.DefaultRequestHeaders.Clear();
                productlist.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await productlist.GetAsync("api/product");

                if (res.IsSuccessStatusCode)
                {
                    var productResponse = res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<List<Product>>(productResponse);
                }
            }
            return View(product);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(int product_id,string product_Name,string category,int price)
        {
            string apiURL = "http://localhost:36582/api/";
            var input = new
            {
                Product_Id=product_id,
                Product_Name = product_Name,
                Category = category,
                Price = price,
            };
            string inputjson= (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            
        //    string json = client.UploadString(apiURL + "/product", inputjson);
          // List<Product> products = (new JavaScriptSerializer()).Deserialize<List<Product>>(json);
            //           return View(products);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetIndustry()
        {
            string RESTAPI_Url = "http://127.0.0.1:8000/";
            List<Industry> getIndustry = new List<Industry>();
            using (var industrylist = new HttpClient())
            {
                industrylist.BaseAddress = new Uri(RESTAPI_Url);
                industrylist.DefaultRequestHeaders.Clear();
                industrylist.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await industrylist.GetAsync("api/industrylist");
                if (res.IsSuccessStatusCode)
                {
                    var industryResponse = res.Content.ReadAsStringAsync().Result;
                    getIndustry = JsonConvert.DeserializeObject<List<Industry>>(industryResponse);
                }
            }
            return View(getIndustry);
        }

        public ActionResult AddIndustry()
        {
            return View();
        }


    }
}