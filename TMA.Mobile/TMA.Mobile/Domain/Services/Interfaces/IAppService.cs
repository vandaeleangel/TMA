﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IAppService
    {
        Task<IEnumerable<Chore>> GetAllChores();
        Task<TimeBlock> StartTimeBlock(AddTimeBlockDto timeBlockDto);
        Task<TimeBlock> StopTimeBlock(UpdateEndTimeDto timeBlockDto);
        Task<IEnumerable<TimeBlock>> GetFilteredTimeBlocks(TimeBlockQueryParametersDto param);
        Task<bool> SaveChangesAsync();
    }
}
