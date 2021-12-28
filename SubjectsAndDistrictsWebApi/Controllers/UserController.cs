using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubjectsAndDistrictsWebApi.BL.Exceptions;
using SubjectsAndDistrictsWebApi.BL.Model;
using SubjectsAndDistrictsWebApi.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserApiDTO>> Get()
        {
            return await userService.GetProfiles();
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserApiDTO>> Get(string userName)
        {
            var profile = await userService.GetProfile(userName);
            if (profile == null) return StatusCode(404, new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userName)));
            else return StatusCode(200, profile);
        }

        [HttpPost]
        public async Task<ActionResult<Exception>> Post([FromBody] UserApiDTOCreate user)
        {
            var ex = await userService.CreateProfile(user);
            if (ex != null)
            {
                if (ex is AlreadyExistException) return StatusCode(409, ex.Message);
                if (ex is CreateUserException) return StatusCode(500, ex.Message);
                if (ex is UserRoleException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpPost("role/{userName}")]
        public async Task<ActionResult<Exception>> PostRoleToUser(string userName, string role)
        {
            var ex = await userService.AddToRole(userName, role);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is UserRoleException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpPut]
        public async Task<ActionResult<Exception>> Put([FromBody] UserApiDTOCreate user)
        {
            var ex = await userService.UpdateProfile(user);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult<Exception>> Delete(string userName)
        {
            var ex = await userService.Delete(userName);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete("role/{userName}")]
        public async Task<ActionResult<Exception>> DeleteRoleFromUser(string userName, string role)
        {
            var ex = await userService.RemoveRole(userName, role);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is UserRoleException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }
    }
}
