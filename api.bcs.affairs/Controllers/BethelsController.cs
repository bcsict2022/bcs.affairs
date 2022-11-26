using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BethelsController : ControllerBase
    {
        private readonly IBethels _repo;
        public BethelsController(IBethels repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("getBethels")]
        public async Task<IActionResult> GetBethels()
        {
            try
            {
                List<BethelLists> result = await _repo.GetBethelsAsync();
                return Ok(result); //result
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }
       
        [HttpGet]
        [Route("getBethels{id}")]
        public async Task<IActionResult> GetBethels(string countryId)
        {
            try
            {
                List<BethelLists> result = await _repo.GetBethelsAsync(countryId);
                return Ok(result); //result
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpGet]
        [Route("getBethelDetails{id}")]
        public async Task<IActionResult> GetBethelDetails(string id)
        {
            try
            {
                BethelLists result = await _repo.GetBethelDetailsAsync(id);
                return Ok(result); //result
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addBethel")]
        public async Task<IActionResult> CreateBethel([FromBody] vmBethel model)
        {
            try
            {
                var response = await _repo.CreateBethelAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPut]
        [Route("modifyBethel")]
        public async Task<IActionResult> EditBethels([FromBody] Bethels model)
        {
            try
            {
                await _repo.EditBethelAsync(model);
                return NoContent();
            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpDelete]
        [Route("deleteBethel{id}")]
        public async Task<IActionResult> RemoveBethel(string id)
        {
            try
            {
                await _repo.DeleteBethelAsync(id);
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
