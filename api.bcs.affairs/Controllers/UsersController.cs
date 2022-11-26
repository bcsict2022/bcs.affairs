using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.bcs.affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _repo;
        public UsersController(IUsers repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<vmUserDetails> result = await _repo.GetUsersAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

      
        [HttpPost]
        [Route("addUser")]
        public async Task<IActionResult> CreateUser([FromBody]vmUser model)
        {
            try
            {
                var response = await _repo.CreateUserAsync(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPut]
        [Route("modifyUser")]
        public async Task<IActionResult> EditUser([FromBody] vmUserDetails model)
        {
            try
            {
                await _repo.EditUserAsync(model);
                return NoContent();


            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpDelete]
        [Route("deleteUser{id}")]
        public async Task<IActionResult> RemoveUser(string id)
        {
            try
            {
                await _repo.DeleteUserAsync(id);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPost]
        [Route("getLogin")]
        public async Task<IActionResult> GetLogin([FromQuery] vmLogin model)
        {
            try
            {
                var response = await _repo.GetLogin(model);
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
