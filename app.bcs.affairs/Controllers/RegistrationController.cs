using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace app.bcs.affairs.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IBCSAffairsService _affairsService;

        public RegistrationController(IBCSAffairsService affairsService)
        {
            _affairsService = affairsService;
        }
        public IActionResult Bethels()
        {
            return View();
        }

        public async Task<IActionResult> GetBethels(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Bethels/getBethels";
                var response = await _affairsService.GetAllAsync<BethelLists[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
       
        public async Task<IActionResult> GetBethelsByCountry(DataSourceLoadOptions loadOptions, string countryId)
        {
            try
            {
                var url = "Bethels/getBethels";
                var response = await _affairsService.GetAllByIdAsync<vmBethelLists[]>(url,countryId);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<IActionResult> GetBethelDetail(string id)
        {
            try
            {
                var url = "Bethels/BethelDetail";
                var response = await _affairsService.GetAllByIdAsync<vmBethelLists[]>(url, id);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public IActionResult NewBethels()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Bethels(vmBethel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var model = new vmBethel()
                    {
                        BethelTypeId = vm.BethelTypeId,
                        BethelDescription = vm.BethelDescription,
                        Address = vm.Address,
                        Address2 = vm.Address2,
                        CountryId = vm.CountryId,
                        StatesProvince = vm.StatesProvince,
                        LocalCouncil = vm.LocalCouncil,
                        ZipPostCode = vm.ZipPostCode,
                        BcsZone = vm.BcsZone,
                        Town = vm.Town,
                        UserId = HttpContext.Session.GetString("_userId")
                    };
                    var response = await _affairsService.CreateAsync("Bethels/addBethel", model);
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
            return View("Bethels", vm); ;
        }

        public async Task<IActionResult> GetBethelStatesProvinces(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Bethels/getBethelStatesProvinces";
                var response = await _affairsService.GetAllAsync<BethelDistinctLists[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<IActionResult> GetBethelLocalCouncils(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Bethels/getBethelLocalCouncils";
                var response = await _affairsService.GetAllAsync<BethelDistinctLists[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<IActionResult> GetBethelTowns(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Bethels/getBethelTowns";
                var response = await _affairsService.GetAllAsync<BethelDistinctLists[]>(url);

                var resultJson = JsonConvert.SerializeObject(response);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public IActionResult EditBethels()
        {
            return View();
        }
        public async Task<IActionResult> EditBethels(string key, string values)
        {
            try
            {
                //if (string.IsNullOrEmpty(HttpContext.Session.GetString("_userId")))
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                var rn = JsonConvert.DeserializeObject<vmBethelLists>(value: values);
                var model = new vmBethelLists()
                {
                    Id = key,
                    BethelTypeId = rn.BethelTypeId,
                    BethelDescription = rn.BethelDescription,
                    Address = rn.Address,
                    Address2 = rn.Address2,
                    CountryId = rn.CountryId,
                    StatesProvince = rn.StatesProvince,
                    LocalCouncil = rn.LocalCouncil,
                    ZipPostCode = rn.ZipPostCode,
                    BcsZone = rn.BcsZone,
                    Town = rn.Town,
                    UserId = HttpContext.Session.GetString("_userId"),
                    TransactionDate = DateTime.Today.Date
                };

                var response = await _affairsService.EditAsync<BethelTypes>("Bethels/modifyBethel", model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async void DeleteBethels(string key)
        {
            try
            {
                var response = await _affairsService.DeleteAsync("Bethels/deleteBethel", key.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
