using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace app.bcs.affairs.Controllers
{
    public class BasetablesController : Controller
    {
        private readonly IBCSAffairsService _affairsService;
        public BasetablesController(IBCSAffairsService affairsService)
        {
            _affairsService = affairsService;
        }

        public async Task<IActionResult> GetCountries(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "ManagerTables/getCountries";
                var response = await _affairsService.GetAllAsync<Countries[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }
        
        public IActionResult Departments()
        {           
            return View();            
        }
        public async Task<IActionResult> GetDepartments(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "ManagerTables/getDepartments";
                var response = await _affairsService.GetAllAsync<Departments[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Departments(vmSetupTable1 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var depart = new vmSetupTable1()
                    //{
                    //    Description = "new test department"
                    //};
                    //using (var httpClient = new HttpClient())
                    //{
                    //    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    //    using (var response = await httpClient.PostAsync("https://localhost:7177/api/ManagerTables/addDepartment", content))
                    //    {
                    //        if (response.IsSuccessStatusCode)
                    //        {
                    //            return Json(new { mstatus = "success" });
                    //        }
                    //    }
                    //}

                    var response = await _affairsService.CreateAsync("ManagerTables/addDepartment", model);
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
            return View("Departments", model);           

        }       
        public async Task<IActionResult> EditDepartments(int key, string values)
        {
            try
            {
               var rn = JsonConvert.DeserializeObject<Departments>(value: values);
               var model = new Departments()
               {
                    Id = key,
                    DepartmentDescription = rn.DepartmentDescription
               };

                var response = await _affairsService.EditAsync<Departments>("ManagerTables/modifyDepartment", model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async void DeleteDepartments(int key)
        {
            try
            {               
                var response = await _affairsService.DeleteAsync("ManagerTables/deleteDepartment", key.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IActionResult BethelTypes()
        {
            return View();
        }

        public async Task<IActionResult> GetBethelTypes(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "ManagerTables/getBetheltypes";
                var response = await _affairsService.GetAllAsync<BethelTypes[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BethelTypes(vmSetupTable1 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _affairsService.CreateAsync("ManagerTables/addBetheltype", model);
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
            return View("BethelTypes", model);

        }

        public async Task<IActionResult> EditBethelTypes(int key, string values)
        {
            try
            {
                var rn = JsonConvert.DeserializeObject<BethelTypes>(value: values);
                var model = new BethelTypes()
                {
                    Id = key,
                    BethelTypeDescription = rn.BethelTypeDescription
                };

                var response = await _affairsService.EditAsync<BethelTypes>("ManagerTables/modifyBetheltype", model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async void DeleteBethelTypes(int key)
        {
            try
            {
                var response = await _affairsService.DeleteAsync("ManagerTables/deleteBetheltype", key.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
