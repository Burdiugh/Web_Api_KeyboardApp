using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;
        public ScoreController(IScoreService scoreService)
        {
            this._scoreService = scoreService;
        }


        [HttpGet]
        public async Task<IActionResult> GetScores()
        {
            return Ok(await _scoreService.GetAllAsync());
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetScoreById([FromRoute] int id)
        {
            return Ok(await _scoreService.GetScoreByIdAsync(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await _scoreService.DeleteByIdAsync(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ScoreDTO score)
        {
            // if (!ModelState.IsValid) return BadRequest();

            await _scoreService.InsertScoreAsync(score);

            return Ok();

        }

        [HttpGet("byUserId/{id}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] string id)
        {
            return Ok(await _scoreService.GetAllByUserId(id));
        }


    }
}
