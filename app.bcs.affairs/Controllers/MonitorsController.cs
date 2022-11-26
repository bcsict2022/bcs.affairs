using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Net.Security;
using System.Text;

namespace app.bcs.affairs.Controllers
{
    public class MonitorsController : Controller
    {
        private readonly IBCSAffairsService _affairsService;
        

        public MonitorsController(IBCSAffairsService affairsService)
        {
            _affairsService = affairsService;          
        }

        public IActionResult Users()
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

        [HttpPost]
        public async Task<IActionResult> Users(vmUser models)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var model = new vmUser()
                    {
                        CreatedUser = HttpContext.Session.GetString("_userId"),
                        FirstName = models.FirstName,
                        LastName = models.LastName,
                        UserId = models.UserId
                    };
                    var response = await _affairsService.CreateAsync("Users/addUser", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { mstatus = "success" });
                    }
                }
            }
            catch (Exception ex)
            {
                Exception current = ex;
                SqlException se = null;
                do
                {
                    se = current.InnerException as SqlException;
                    current = current.InnerException;
                }
                while (current != null && se == null);

                if (se != null)
                {
                    return Json(new { mstatus = se.Message.ToString() });
                }
                else
                {
                    return Json(new { mstatus = ex.Message.ToString() });
                }
            }
            return View("Users", models); ;
        }

        public async Task<IActionResult> EditUsers(string key, string values)
        {
            try
            {
                //if (string.IsNullOrEmpty(HttpContext.Session.GetString("_userId")))
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                var rn = JsonConvert.DeserializeObject<vmUserDetails>(value: values);
                var model = new vmUserDetails()
                {
                    EmailAddress = key,
                    FirstName   = rn.FirstName,
                    LastName = rn.LastName,
                    UserStatus = rn.UserStatus,
                    IsFirstLogin = rn.IsFirstLogin,
                    CreatedUser = rn.CreatedUser,
                    TransactionDate = rn.TransactionDate
                };

                var response = await _affairsService.EditAsync<BethelTypes>("Users/modifyUser", model);
               
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async void DeleteUsers(string key)
        {
            try
            {
                var response = await _affairsService.DeleteAsync("Users/deleteUser", key.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
