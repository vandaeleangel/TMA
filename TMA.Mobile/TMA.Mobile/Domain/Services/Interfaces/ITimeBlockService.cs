using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface ITimeBlockService
    {
        Task<IEnumerable<TimeBlock>> GetFilteredTimeBlocks(TimeBlockQueryParametersDto param);
        Task<TimeBlock> AddNewTimeBlock(AddTimeBlockDto newTimeBlock);
        Task<string> DeleteTimeBlock(Guid timeBlockId);
        Task<TimeBlock> UpdateTimeBlock(UpdatedTimeBlockDto updatedTimeBlock);
    }
}
