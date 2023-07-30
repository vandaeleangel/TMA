﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Core.Data;
using TMA.Core.Entities;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.Chore;
using TMA.SharedDtos.Dtos.TimeBlock;

namespace TMA.Core.Services
{
    public class TimeBlockService : ITimeBlockService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TimeBlockService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
  
        public async Task<GetTimeBlockDto> AddTimeBlock(AddTimeBlockDto timeBlock)
        {
            var response = new GetTimeBlockDto();

            if (timeBlock.ChoreId != Guid.Empty)
            {
                Chore chore = await _context.Chores.FirstOrDefaultAsync(c => c.Id == timeBlock.ChoreId);

                TimeBlock newTimeBlock = new TimeBlock
                {
                    StartTime = DateTime.Now,
                    Duration = TimeSpan.Zero,
                    ChoreId = timeBlock.ChoreId,
                    Chore = chore               
                };

                _context.TimeBlocks.Add(newTimeBlock);
                await _context.SaveChangesAsync();
                
                response = _mapper.Map<GetTimeBlockDto>(newTimeBlock);

                return response;

            }
            else return null;
        }

        public async Task<GetTimeBlockDto> DeleteTimeBlock(Guid id)
        {
            var result = new GetTimeBlockDto();
            
            var dbTimeBlock = await _context.TimeBlocks.FirstOrDefaultAsync(t => t.Id == id);
            if (dbTimeBlock != null)
            {
                _context.TimeBlocks.Remove(dbTimeBlock);
                await _context.SaveChangesAsync();

                result = _mapper.Map<GetTimeBlockDto>(dbTimeBlock);
                return result;
            }
            else return null;
        }

        public async Task<List<GetTimeBlockDto>> GetAllTimeBlocks()
        {
           var response = new List<GetTimeBlockDto>();

           var dbTimeBlocks = await _context.TimeBlocks
                .Include(t => t.Chore)
                .ToListAsync();

            if (dbTimeBlocks != null)
            {
                response = dbTimeBlocks.Select(t => _mapper.Map<GetTimeBlockDto>(t)).ToList();
                return response;
            }
            else return null;
        }

        public async Task<GetTimeBlockDto> GetTimeBlockById(Guid id)
        {
            var response = new GetTimeBlockDto();

            var dbTimeBlock = await _context.TimeBlocks
                .Include(t => t.Chore)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (dbTimeBlock != null)
            {
                response = _mapper.Map<GetTimeBlockDto>(dbTimeBlock);
                return response;
            }
            else return null;
        }

        public async Task<GetTimeBlockDto> UpdateEndTime(UpdateEndTimeDto timeBlock)
        {
            var response = new GetTimeBlockDto();

            var dbTimeBlock = await _context.TimeBlocks.FirstOrDefaultAsync(t => t.Id == timeBlock.TimeBlockId);
            if (dbTimeBlock != null)
            {
                
                dbTimeBlock.StartTime = dbTimeBlock.StartTime;
                dbTimeBlock.EndTime = DateTime.Now;
                dbTimeBlock.Chore = dbTimeBlock.Chore;
                dbTimeBlock.ChoreId = dbTimeBlock.ChoreId;
                dbTimeBlock.Duration = dbTimeBlock.EndTime - dbTimeBlock.StartTime;

                _context.TimeBlocks.Update(dbTimeBlock);
             
                Chore chore = await _context.Chores.FirstOrDefaultAsync(c => c.Id == dbTimeBlock.ChoreId);
                chore.Duration += dbTimeBlock.Duration;

                await _context.SaveChangesAsync();

                response = _mapper.Map<GetTimeBlockDto>(dbTimeBlock);
                return response;
            }
            else return null;
        }

        public Task<GetTimeBlockDto> UpdateTimeBlock(UpdateTimeBlockDto timeBlock)
        {
            throw new NotImplementedException();
        }

    }
}
