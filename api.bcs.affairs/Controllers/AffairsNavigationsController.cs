using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffairsNavigationsController : ControllerBase
    {
        private readonly IAffairsNavigations _repo;
        public AffairsNavigationsController(IAffairsNavigations repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("getProfileMenuItems{id}")]
        public async Task<IActionResult> GetProfileMenuItems(int id)
        {
            try
            {
                List<MenuCommands> result = await _repo.GetProfileMenuItems(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }
    }
}
