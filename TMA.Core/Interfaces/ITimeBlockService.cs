using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.TimeBlock;

namespace TMA.Core.Interfaces
{
    public interface ITimeBlockService
    {
        Task<List<GetTimeBlockDto>> GetAllTimeBlocks();
        Task<GetTimeBlockDto> GetTimeBlockById(Guid id);
        Task<GetTimeBlockDto> DeleteTimeBlock(Guid id);
        Task<GetTimeBlockDto> AddTimeBlock(AddTimeBlockDto choreId);
        Task<GetTimeBlockDto> UpdateEndTime(UpdateEndTimeDto choreId);

        Task<GetTimeBlockDto> UpdateTimeBlock(UpdateTimeBlockDto timeBlock);

    }
}
