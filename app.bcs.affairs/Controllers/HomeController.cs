using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;

namespace app.bcs.affairs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;      
        //private readonly IHttpClientFactory _clientFactory;
        private readonly IBCSAffairsService _affairsService;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IBCSAffairsService  affairsService, IWebHostEnvironment hostEnvironment, IConfiguration iConfig)
        {
            _logger = logger;
            _affairsService = affairsService;
            _hostingEnvironment = hostEnvironment;
            _configuration = iConfig;
        }

        public async Task<IActionResult> Index()
        {
            List<populations> countsList = new List<populations>();
            var url = "Bethels/getBethelCounts";
            var populations = await _affairsService.GetAllAsync<populations[]>(url);
            if (populations.Count() > 0)
            {
                countsList = populations.ToList();


                using (var writer = System.IO.File.CreateText(_hostingEnvironment.ContentRootPath + "\\wwwroot\\Content\\Populations.js"))
                {
                    writer.WriteLine("var populations = {");
                    for (var i = 0; i < countsList.Count; i++)
                    {
                        writer.WriteLine(countsList[i].name);
                    }
                    writer.WriteLine("};");
                }
            }
            return View("Index");
        }
       
        [HttpPost]
        public async Task<IActionResult> Login(vmLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string login = null; // = _affairsService.CreateAsync);

                    var response = await _affairsService.GetTypedObjectAsync<UserNameDetails>("Users/getLogin", model);
                   

                    if (response != null)
                    {
                        login = response.UserFullName;

                        if (string.IsNullOrEmpty(login))
                        {
                            ViewBag.loginError = "The user login or password provided is incorrect.";
                            return View("Index", model);
                        }
                        else if ((login == "Password Not matched"))
                        {
                            ViewBag.loginError = "Password Not matched";
                            return View("Index", model);
                        }
                        else if (login == "User Status is INACTIVE")
                        {
                            ViewBag.loginError = "User Status is INACTIVE";
                            return View("Index", model);
                        }
                        else if (login == "User Name not found")
                        {
                            ViewBag.loginError = "The user Name is incorrect.";
                            return View("Index", model);
                        }
                        else
                        {
                            var loginTransaction = new vmLoginTransaction()
                            {
                                UserId = model.UserId,
                                TransactionType = "Sign In"
                            };
                            var responseLogin = await _affairsService.CreateAsync("Users/addLoginTransaction", loginTransaction);


                            HttpContext.Session.SetString("_userName", login);
                            HttpContext.Session.SetString("_userId", model.UserId);


                            vmLoginUserProfile userProfile = await _affairsService.GetAllByIdAsync<vmLoginUserProfile>("Users/getLoginUserProfile", model.UserId);
                            if (userProfile != null)
                            {
                                if (!(userProfile.DepartmentId == null))
                                {
                                    HttpContext.Session.SetString("_departmentId", userProfile.DepartmentId);
                                }
                                else
                                {
                                    ViewBag.loginError = "No Unit/Command";
                                }
                                if (userProfile.BandId > 0)
                                {
                                    HttpContext.Session.SetInt32("_bandId", userProfile.BandId);
                                }
                                else
                                {
                                    ViewBag.loginError = "No Permission";
                                }
                            }

                            var userDepartment = HttpContext.Session.GetString("_departmentId");
                            if (!string.IsNullOrEmpty(userDepartment))
                            {
                                var claims = new List<Claim> { new Claim(ClaimTypes.Name, HttpContext.Session.GetString("_userId")) };
                                var userIdentity = new ClaimsIdentity(claims, "login");
                                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                                await  HttpContext.SignInAsync(principal);

                                switch (userDepartment)
                                {
                                    case "Registration":
                                        return RedirectToAction("RegistrationDashBoards", "Home");
                                    case "Tresury":
                                        Console.WriteLine("Case 2");
                                        break;
                                    default:
                                        return View("Index", model);                                       
                                }
                                
                            }
                            return View("Index", model);
                        }
                    }
                  
                }
            }
            catch (Exception ex)
            {
                ViewBag.loginError = ex.Message;
            }
            return View("Index", model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(vmPasswordChange vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new PasswordChange()
                    {
                       UserId = HttpContext.Session.GetString("_userId"),
                       FormerPassword = vm.FormerPassword,
                       NewPassword = vm.NewPassword,
                       ConfirmPassword = vm.ConfirmPassword
                    };
                    var response = await _affairsService.CreateAsync("Users/addmodifyUserPasswords", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.loginError = "Invalid User Name and/or Password";
                        return View(vm);
                    }
                }
                return View(vm);

            }
            catch (Exception ex)
            {
                ViewBag.loginError = ex.Message.ToString();
                return View(vm);
            }         
        }

        [Authorize]
        public async Task<IActionResult> SignOff()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DashBoards()
        {
            var userDepartment = HttpContext.Session.GetString("_departmentId");
            if (!string.IsNullOrEmpty(userDepartment))
            {
                
                switch (userDepartment)
                {
                    case "Registration":
                        return RedirectToAction("RegistrationDashBoards", "Home");                       
                    case "Tresury":
                        Console.WriteLine("Case 2");
                        break;
                    default:
                        return RedirectToAction("Index", "Home");                      
                }                
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> RegistrationDashBoards()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("_userId")))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {              

                using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\wwwroot\\Content\\GeoLite2-City.mmdb"))
                {

                    var ipAddress = "169.159.107.241"; //HttpContext.Connection.RemoteIpAddress.ToString(); //"169.159.107.241"; //

                    var city = reader.City(ipAddress);
                    var ip = new UserIPDetails
                    {
                        UserId = HttpContext.Session.GetString("_userId"),
                        City = city.City.Name,
                        IPAddress = ipAddress,
                        AccuracyRadius = city.Location.AccuracyRadius.ToString(),
                        StateName = city.MostSpecificSubdivision.Name,
                        Continent = city.Continent.Name,
                        Country = city.Country.Name,
                        Latitude = city.Location.Latitude.ToString(),
                        Longitude = city.Location.Longitude.ToString(),
                        TimeZone = city.Location.TimeZone,
                        RegisteredCountry = city.RegisteredCountry.Name,
                        TransactionDate = DateTime.Today.Date
                    };

                    var response = await _affairsService.CreateAsync("Users/addUserIPDetails", ip);
                }

                //GetDashBoardAggregates dashBoardAggregates = _repoNav.GetDashBoardAggregates(HttpContext.Session.GetString("_CompanyId"));
                //if (dashBoardAggregates != null)
                //{
                //    return View(dashBoardAggregates);
                //}
                return View();
            }

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