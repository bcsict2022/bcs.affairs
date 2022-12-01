using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandPermissionsController : ControllerBase
    {
        private readonly IBandPermissions _repo;
        public BandPermissionsController(IBandPermissions repo)
        {
            _repo = repo;
        }     

        [HttpGet]
        [Route("getProfiles")]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                List<Bands> result = await _repo.GetProfiles();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpGet]
        [Route("getUserProfiles{id}")]
        public async Task<IActionResult> GetUserProfiles(string id)
        {
            try
            {
                UserBandLists result = await _repo.GetUserProfiles(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] vmBand model)
        {
            try
            {
                var response = await _repo.CreateProfiles(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPost]
        [Route("addEditUserProfiles")]
        public async Task<IActionResult> CreateEditUserProfiles([FromBody] vmUserBand model)
        {
            try
            {
                var response = await _repo.CreateEditUserProfiles(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPut]
        [Route("modifyProfiles")]
        public async Task<IActionResult> EditProfile([FromBody] Bands model)
        {
            try
            {
                await _repo.EditProfiles(model);
                return NoContent();


            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpDelete]
        [Route("deleteProfile{id}")]
        public async Task<IActionResult> RemoveProfile(int id)
        {
            try
            {
                await _repo.DeleteProfiles(id);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }     
    }
}
