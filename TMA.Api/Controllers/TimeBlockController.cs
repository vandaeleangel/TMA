using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.TimeBlock;

namespace TMA.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeBlockController : ControllerBase
    {
        private readonly ITimeBlockService _timeBlockService;
        public TimeBlockController(ITimeBlockService timeBlockService)
        {
            _timeBlockService = timeBlockService;
        }

        [HttpPost]
        public async Task<ActionResult<GetTimeBlockDto>> AddTimeBlock(AddTimeBlockDto newTimeBlock)
        {
            var result = await _timeBlockService.AddTimeBlock(newTimeBlock);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }
    }
}
