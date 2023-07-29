using AutoMapper;
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
                //Chore chore = 

                TimeBlock newTimeBlock = new TimeBlock
                {
                    StartTime = DateTime.Now,
                    Duration = TimeSpan.Zero,
                    ChoreId = timeBlock.ChoreId,
                    Chore = await _context.Chores.FirstOrDefaultAsync(c => c.Id == timeBlock.ChoreId)             
                };

                _context.TimeBlocks.Add(newTimeBlock);
                await _context.SaveChangesAsync();
                
                response = _mapper.Map<GetTimeBlockDto>(newTimeBlock);
                return response;

            }
            else return null;
        }

        public Task<GetTimeBlockDto> DeleteTimeBlock(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetTimeBlockDto>> GetAllTimeBlocks()
        {
            throw new NotImplementedException();
        }

        public Task<GetTimeBlockDto> GetTimeBlockById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetTimeBlockDto> UpdateEndTime(UpdateEndTimeDto choreId)
        {
            throw new NotImplementedException();
        }

        public Task<GetTimeBlockDto> UpdateTimeBlock(UpdateTimeBlockDto timeBlock)
        {
            throw new NotImplementedException();
        }
    }
}
