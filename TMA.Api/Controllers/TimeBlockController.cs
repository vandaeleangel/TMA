using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos;
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetTimeBlockDto>>> GetAllTimeBlocks()
        {
            var result = await _timeBlockService.GetAllTimeBlocks();

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTimeBlockDto>> GetTimeBlockById(Guid id)
        {
            var result = await _timeBlockService.GetTimeBlockById(id);

            if (result is not null) return Ok(result);
            else return NotFound();
        }

        [HttpGet("GetFiltered")]
        public async Task<ActionResult<List<GetTimeBlockDto>>> GetFilteredTimeBlocks([FromQuery]TimeBlockQueryParametersDto parameters)
        {
            var result = await _timeBlockService.GetAllTimeBlocksFiltered(parameters);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<GetTimeBlockDto>> AddTimeBlock(AddTimeBlockDto newTimeBlock)
        {
            var result = await _timeBlockService.AddTimeBlock(newTimeBlock);

            if (result is not null) return Ok(result);
            else return BadRequest();
        }

        [HttpPut("EndTime")]
        public async Task<ActionResult<GetTimeBlockDto>> UpdateEndTimeOfTimeBlockofChore(UpdateEndTimeDto updateEndTimeDto)
        {
            var result = await _timeBlockService.UpdateEndTime(updateEndTimeDto);

            if (result is not null) return Ok(result);
            else return NotFound(result);
        }

        [HttpPut]
        public async Task<ActionResult<GetTimeBlockDto>> UpdateTimeBlock(UpdateTimeBlockDto updateTimeBlockDto)
        {
            var result = await _timeBlockService.UpdateTimeBlock(updateTimeBlockDto);

            if (result is not null) return Ok(result);
            else return NotFound(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<GetTimeBlockDto>> DeleteTimeBlock(Guid id)
        {
            var result = await _timeBlockService.DeleteTimeBlock(id);

            if (result is not null) return Ok(result);
            else return BadRequest(result);
        }
    }
}
