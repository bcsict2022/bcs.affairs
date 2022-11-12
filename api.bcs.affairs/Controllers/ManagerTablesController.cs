using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerTablesController : ControllerBase
    {
        private readonly ISetupManagerTables _repo;
        public ManagerTablesController(ISetupManagerTables repo)
        {
            _repo = repo;
        }

        #region countries

        [HttpGet]
        [Route("getCountries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                List<Countries> result = await _repo.GetCountriesAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        #endregion

        #region departments

        [HttpGet]
        [Route("getDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                List<Departments> result = await _repo.GetDepartmentsAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] vmSetupTable1 model)
        {
            try
            {
                var response = await _repo.CreateDepartmentAsync(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPut]
        [Route("modifyDepartment")]
        public async Task<IActionResult> EditDepartment([FromBody] Departments model)
        {
            try
            {
                await _repo.EditDepartmentAsync(model);
                return NoContent();


            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpDelete]
        [Route("deleteDepartment")]
        public async Task<IActionResult> RemoveDepartment([FromQuery] int id)
        {
            try
            {
                await _repo.DeleteDepartmentAsync(id);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }
        #endregion
    }
}
