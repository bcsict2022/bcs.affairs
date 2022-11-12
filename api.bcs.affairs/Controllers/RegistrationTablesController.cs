using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationTablesController : ControllerBase
    {
        private readonly ISetupRegistrationTables _repo;
        public RegistrationTablesController(ISetupRegistrationTables repo)
        {
            _repo = repo;
        }

        #region bethelType

        [HttpGet]
        [Route("getBethelTypes")]
        public async Task<IActionResult> GetBethelTypes()
        {
            try
            {
                List<BethelTypes> result = await _repo.GetBethelTypesAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addBethelType")]
        public async Task<IActionResult> CreateBethelType([FromBody] vmSetupTable1 model)
        {
            try
            {
                var response = await _repo.CreateBethelTypeAsync(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPut]
        [Route("modifyBethelType")]
        public async Task<IActionResult> EditBethelType([FromBody] BethelTypes model)
        {
            try
            {
                await _repo.EditBethelTypeAsync(model);
                return NoContent();


            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpDelete]
        [Route("deleteBethelType")]
        public async Task<IActionResult> RemoveBethelType([FromQuery] int id)
        {
            try
            {
                await _repo.DeleteBethelTypeAsync(id);
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
