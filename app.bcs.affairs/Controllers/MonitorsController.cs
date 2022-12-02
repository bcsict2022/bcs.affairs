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
using System.Reflection;
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
        public async Task<IActionResult> Users(vmUser2 models)
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
                        UserId = models.UserId,
                        DepartmentId = models.DepartmentId
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
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_userId")))
                {
                    return RedirectToAction("Index", "Home");
                }
                var rn = JsonConvert.DeserializeObject<vmUserDetails>(value: values);
                var model = new vmEditUser()
                {
                    EmailAddress = key,
                    FirstName   = rn.FirstName,
                    LastName = rn.LastName,
                    DepartmentId=rn.DepartmentId
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

        public IActionResult Profiles()
        {
            return View();
        }
        public async Task<IActionResult> GetProfiles(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "BandPermissions/getProfiles";
                var response = await _affairsService.GetAllAsync<Bands[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Profiles(vmBand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _affairsService.CreateAsync("BandPermissions/addProfile", model);
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
            return View("Profiles", model);
        }       
        public async Task<IActionResult> EditProfiles(int key, string values)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_userId")))
                {
                    return RedirectToAction("Index", "Home");
                }
                var rn = JsonConvert.DeserializeObject<Bands>(value: values);
                var model = new Bands()
                {
                    Id = key,
                    Name = rn.Name
                };

                var response = await _affairsService.EditAsync<Bands>("BandPermissions/modifyProfiles", model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async void DeleteProfiles(int key)
        {
            try
            {
                var response = await _affairsService.DeleteAsync("BandPermissions/deleteProfile", key.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public IActionResult ProfilePermissions()
        {
            return View();
        }       
        public async Task<IActionResult> GetMenuCommands(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "AffairsNavigations/getMenuCommands";
                var response = await _affairsService.GetAllAsync<MenuCommands[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }
        public async Task<IActionResult> GetProfileMenuCommands(DataSourceLoadOptions loadOptions, int id)
        {
            try
            {
                var url = "AffairsNavigations/getProfileMenuCommands";
                var response = await _affairsService.GetAllByIdAsync<GetProfileMenuPermissions[]>(url, id.ToString());

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }
      
        [HttpPost]
        public async Task<IActionResult> ProfilePermissions(string menuGroup, string[] menuHeads, string[] menuLines)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mh = null;
                    string ml = null;

                    for (int i = 0; i < menuHeads.Length; i++)
                    {
                        mh = menuHeads[i];
                    }

                    for (int i = 0; i < menuLines.Length; i++)
                    {
                        ml = menuLines[i];
                    }

                    var model = new ProfileMenus()
                    {
                        menuGroup = menuGroup,
                        menuHeads = mh,
                        menuLines = ml
                    };
                    var response = await _affairsService.CreateAsync("AffairsNavigations/addProfileMenus", model);
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
            return View("ProfilePermissions");
        }

        public IActionResult UsersProfile()
        {
            return View();
        }
        public async Task<JsonResult> GetUserProfile(string Id)
        {
            try
            {
                vmLoginUserProfile userProfile = await _affairsService.GetAllByIdAsync<vmLoginUserProfile>("Users/getLoginUserProfile", Id);
                if (userProfile != null)
                {
                    return Json(new
                    {
                        id = userProfile.UserId,
                        descriptions = userProfile.BandName
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return Json(new { mstatus = ex.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UsersProfile(vmUserBand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _affairsService.CreateAsync("BandPermissions/addEditUserProfiles", model);
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
            return View("UsersProfile", model);
        }

    }
}
