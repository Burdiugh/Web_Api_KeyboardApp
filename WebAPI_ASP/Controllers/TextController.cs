using Core;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_ASP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;
        public TextController(ITextService _textService)
        {
            this._textService = _textService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllText()
        {
            return Ok(await _textService.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTextById([FromRoute] int id)
        {
            return Ok(await _textService.GetTextByIdAsync(id));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await _textService.DeleteTextByIdAsync(id);

            return Ok($"Text with id \"{id}\" was successffuly Deleted!");
        }

        [HttpPost("insert")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Insert([FromBody] AppTextDTO text)
        {

            await _textService.InsertTextAsync(text);

            return Ok($"Text was successffuly added!");
        }

        [HttpPut]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] AppTextDTO text)
        {

            await _textService.UpdateTextAsync(text);

            return Ok($"Text with id #{text.Id} / Level: {text.LevelName} / Language: {text.LanguageName} was successffuly Updated!");
        }

    }
}
