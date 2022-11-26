using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace app.bcs.affairs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static HttpClient _httpClient;

        private readonly IHttpClientFactory _clientFactory;

        private readonly IBCSAffairsService _affairsService;

        //static HomeController()
        //{
        //    _httpClient = new HttpClient();
        //}
        //public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        //{
        //    _logger = logger;
        //    _clientFactory = clientFactory;
        //}

        public HomeController(ILogger<HomeController> logger, IBCSAffairsService  affairsService)
        {
            _logger = logger;
            _affairsService = affairsService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Users/getUser";
                var response = await _affairsService.GetAllAsync<vmUserDetails[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }


        [HttpGet]
        public async Task<string> GetUserById(int id)
        {
            var url = $"https://localhost:7177/api/{id}";

            var response = await _httpClient.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return apiResponse;
        }

        [HttpGet] //best method 1
        public async Task<string> GetUserById2(int id)
        {
            //var httpclient = _clientFactory.CreateClient();
            //var url = $"https://localhost:7177/api/{id}";
            var httpclient = _clientFactory.CreateClient("bcs_affairs_api");
            
            var url = $"{id}";

            var response = await httpclient.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return apiResponse;
        }


        [HttpGet] //best method 1*****
        public async Task<string> GetUserById3(string id)
        {
            return await _affairsService.GetAsync(id);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}