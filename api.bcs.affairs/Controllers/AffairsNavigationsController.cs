using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
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

        [HttpGet]
        [Route("getProfileMenuCommands{id}")]
        public async Task<IActionResult> GetProfileMenuCommands(int id)
        {
            try
            {
                List<GetProfileMenuPermissions> result = await _repo.GetProfileMenuCommands(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpGet]
        [Route("getMenuCommands")]
        public async Task<IActionResult> GetMenuCommands()
        {
            try
            {
                List<MenuCommands> result = await _repo.GetMenuCommands();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addProfileMenus")]
        public async Task<IActionResult> CreateProfileMenus([FromBody] ProfileMenus vm) //string menuGroup, string menuHeads, string menuLines
        {
            try
            {                
                var response = await _repo.CreateProfileMenus(vm);
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
