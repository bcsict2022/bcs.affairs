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
        public async Task<IActionResult> EditUser([FromBody] vmEditUser model)
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
        public async Task<IActionResult> GetLogin([FromBody] vmLogin model)
        {
            try
            {
                UserNameDetails result = await _repo.GetLogin(model);
                return Ok(result);

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPost]
        [Route("addLoginTransaction")]
        public async Task<IActionResult> GetLogin([FromBody] vmLoginTransaction model)
        {
            try
            {
                var response = await _repo.LoginTransactions(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpGet]
        [Route("getLoginUserProfile{id}")]
        public async Task<IActionResult> GetLoginUserProfile(string id)
        {
            try
            {
                vmLoginUserProfile result = await _repo.GetUserProfile(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }

        }

        [HttpPost]
        [Route("addUserIPDetails")]
        public async Task<IActionResult> CreateUserIPDetails([FromBody] UserIPDetails model)
        {
            try
            {
                var response = await _repo.CreateUserIPDetails(model);
                return Ok();

            }
            catch (Exception e)
            {
                var mmmm = e.Message.ToString();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpPost]
        [Route("addmodifyUserPasswords")]
        public async Task<IActionResult> EditUserPasswords([FromBody] vmPasswordChange model)
        {
            try
            {
                var response = await _repo.EditUserPasswords(model);
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
