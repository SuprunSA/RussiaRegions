using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SubjectsAndDistrictsDbContext.Model.DTO;
using SubjectsAndDistrictsWebApi.BL.Exceptions;
using SubjectsAndDistrictsWebApi.BL.Model;
using SubjectsAndDistrictsWebApi.BL.Services;
using SubjectsAndDistrictsWebApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService userService;
        private readonly SignInManager<UserDbDTO> signInManager;

        public AccountController(UserService userService, SignInManager<UserDbDTO> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded) return StatusCode(401);
            else return StatusCode(200);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        [Authorize]
        [HttpGet]
        public UserApiDTO Get()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            return new UserApiDTO()
            {
                UserName = identity.Name,
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = identity.FindFirst("FirstName")?.Value,
                MiddleName = identity.FindFirst("MiddleName")?.Value,
                LastName = identity.FindFirst("LastName")?.Value
            };
        }

        [Authorize]
        [HttpGet("roles")]
        public string GetRoles()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            return identity.FindFirst(ClaimTypes.Role)?.Value;
        }

        [Authorize]
        [HttpPost("password")]
        public async Task<ActionResult<Exception>> ResetPassword([FromForm] string password)
        {
            var ex = await userService.ResetPassword(HttpContext.User.Identity.Name, password);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Exception>> UpdateProfile([FromBody] UserApiDTO user)
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
    }
}
