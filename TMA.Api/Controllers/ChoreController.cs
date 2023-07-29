using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.Chore;

namespace TMA.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChoreController : ControllerBase
    {
        private readonly IChoreService _choreService;

        public ChoreController(IChoreService choreService)
        {
            _choreService = choreService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetChoreDto>>> GetAllChores()
        {
            var result = await _choreService.GetAllChores();

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<GetChoreDto>> AddChore(AddChoreDto newChore)
        {
            var result = await _choreService.AddChore(newChore);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }
    }
}
