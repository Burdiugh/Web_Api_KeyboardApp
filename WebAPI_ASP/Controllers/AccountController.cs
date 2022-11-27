using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO userData)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _accountService.RegisterAsync(userData);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _accountService.LoginAsync(data.Login, data.Password);
            var response = await _accountService.LoginAsync(data.Login, data.Password);

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return Ok();
        }


        [HttpGet("images/{id}")]
        public IActionResult GetImage([FromRoute] string id)
        {
            var fileInfo = _accountService.GetImage(id);
            return File(fileInfo.Stream, fileInfo.ContentType, fileInfo.FileName);
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] RequestResetPasswordDTO request)
        {
            await _accountService.RequestResetPassword(request.Email);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            await _accountService.ResetPassword(resetPasswordDTO);

            return Ok();
        }
    }
}
