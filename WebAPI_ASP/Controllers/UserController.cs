using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using BusinessLogicService;
using Core.Interfaces;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.DTOs;



namespace WebAPI_ASP.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private IAppUserService _userService;
        public UserController(IAppUserService _userService)
        {
            this._userService = _userService;
        }


        [Authorize(Roles = "user, admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        //[Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return  Ok(await _userService.GetAllAsync());
        }

        [Authorize(Roles = "user, admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Add([FromBody] AppUserDTO user)
        {

            await _userService.InsertAsync(user);

            return Ok($"{user.UserName} was successffuly added!");

        }

        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        //[Route("update")]
        public async  Task<IActionResult> Update([FromBody] AppUserDTO user)    
        {
           // if (!ModelState.IsValid) return BadRequest();

            await _userService.UpdateAsync(user);

            return Ok($"{user.UserName} was successffuly Updated!");
        }


        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] string id)
        {
           // if (id<0) return NotFound();

            await _userService.DeleteByIdAsync(id);

            return Ok($"User with id \"{id}\" was successffuly Deleted!");
        }


        //[HttpGet("images/{id}")]
        //public IActionResult GetImage([FromRoute] string id)
        //{
        //    var fileInfo = _userService.GetImage(id);
        //    return File(fileInfo.Stream, fileInfo.ContentType, fileInfo.FileName);
        //}
    }
}
