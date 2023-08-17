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
        Task StartTimeBlock(AddTimeBlockDto timeBlockDto);
        Task<TimeBlock> StopTimeBlock(UpdateEndTimeDto timeBlockDto);
        Task<bool> SaveChangesAsync();
    }
}
