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

        [HttpGet("id")]
        public async Task<ActionResult<GetChoreDto>> GetSingleChore(Guid id)
        {
            var result = await _choreService.GetChoreById(id);
            if (result is not null) return Ok(result);
            else return NotFound();
        }
        [HttpGet("current")]
        public async Task<ActionResult<GetChoreDto>> GetCurrentChore()
        {
            var result = await _choreService.GetCurrentChore();
            if (result is not null) return Ok(result);
            else return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<GetChoreDto>> AddChore(AddChoreDto newChore)
        {
            var result = await _choreService.AddChore(newChore);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<GetChoreDto>> UpdateChoreName(UpdateChoreDto updateChore)
        {
            var result = await _choreService.UpdateChore(updateChore);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GetChoreDto>> DeleteChore(Guid id)
        {
            var result = await _choreService.DeleteChore(id);

            if (result is not null) return Ok(result);
            else return NotFound();
        }
    }
}
