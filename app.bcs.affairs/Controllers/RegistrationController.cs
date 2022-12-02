using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using DevExtreme.AspNet.Data;
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
        

        public IActionResult NewBethels()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Bethels(vmBethel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //var model = new vmBethel()
                    //{
                    //    BethelTypeId = vm.BethelTypeId,
                    //    BethelDescription = vm.BethelDescription,
                    //    Address = vm.Address,
                    //    Address2 = vm.Address2,
                    //    CountryId = vm.CountryId,
                    //    StatesProvince = vm.StatesProvince,
                    //    LocalCouncil = vm.LocalCouncil,
                    //    ZipPostCode = vm.ZipPostCode,
                    //    BcsZone = vm.BcsZone,
                    //    Town = vm.Town,
                    //    UserId = HttpContext.Session.GetString("_userId")
                    //};
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
            return View("Bethels", model); ;
        }

        public async Task<IActionResult> GetBethelStatesProvinces(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var url = "Bethels/getBethelStatesProvinces";
                var response = await _affairsService.GetAllAsync<BethelDistinctLists[]>(url);
                var result = DataSourceLoader.Load(response, loadOptions);
                var resultJson = JsonConvert.SerializeObject(result);

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

                var result = DataSourceLoader.Load(response, loadOptions);
                var resultJson = JsonConvert.SerializeObject(result);
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

                var result = DataSourceLoader.Load(response, loadOptions);
                var resultJson = JsonConvert.SerializeObject(result);
                return Content((string)resultJson, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
       
        public async Task<IActionResult> EditBethels(string id)
        {
            try
            {
                vmBethelEdit response = await _affairsService.GetAllByIdAsync<vmBethelEdit>("Bethels/getBethelDetails", id.ToString());
                if (!(response == null))
                {
                    return View(model: response);
                }
             
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditBethel(vmBethelEdit model)
        {
            try
            {
                var response = await _affairsService.EditAsync<vmBethelEdit>("Bethels/modifyBethel", model);

                return RedirectToAction("Bethels");
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
