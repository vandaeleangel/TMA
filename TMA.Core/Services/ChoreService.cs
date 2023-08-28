using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMA.Core.Data;
using TMA.Core.Entities;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.Chore;


namespace TMA.Core.Services
{
    public class ChoreService : IChoreService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ChoreService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<GetChoreDto> AddChore(AddChoreDto chore)
        {
            var response = new GetChoreDto();

            if (!chore.Name.IsNullOrEmpty())
            {
                Chore newChore = new Chore
                {
                    Name = chore.Name,
                    Color = chore.Color,
                    TimeBlocks = new List<TimeBlock>(),
                    Duration = TimeSpan.Zero,
                    User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId())
                };
                _context.Chores.Add(newChore);
                await _context.SaveChangesAsync();

                response = _mapper.Map<GetChoreDto>(newChore);
                return response;

            }
            else return null;
        }

        public async Task<GetChoreDto> DeleteChore(Guid id)
        {
            var response = new GetChoreDto();

            Chore dbChore = await _context.Chores
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbChore != null && dbChore.User.Id == GetUserId())
            {
                _context.Chores.Remove(dbChore);
                await _context.SaveChangesAsync();

                response = _mapper.Map<GetChoreDto>(dbChore);
                return response;
            }
            else return null;

        }

        public async Task<List<GetChoreDto>> GetAllChores()
        {
            var response = new List<GetChoreDto>();

            var chores = await _context.Chores
                .Where(c => c.User.Id == GetUserId())
                .Include(c => c.TimeBlocks)
                .ToListAsync();

            if (chores != null)
            {
                response = chores.Select(c => _mapper.Map<GetChoreDto>(c)).ToList();
                return response;
            }
            else return null;

        }

        public async Task<GetChoreDto> GetChoreById(Guid id)
        {
            var response = new GetChoreDto();

            var dbChore = await _context.Chores
                .Include(c => c.TimeBlocks)
                .FirstOrDefaultAsync(c => c.User.Id == GetUserId() && c.Id == id);
                
            if (dbChore != null)
            {
                response = _mapper.Map<GetChoreDto>(dbChore);
                return response;
            }
            else return null;
        }

        public async Task<GetChoreDto> UpdateChore(UpdateChoreDto chore)
        {
            var response = new GetChoreDto();

            Chore dbChore = await _context.Chores
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == chore.Id);

            if (dbChore != null)
            {
                dbChore.Name = chore.Name;
                dbChore.TimeBlocks = dbChore.TimeBlocks;
                dbChore.Duration = dbChore.Duration;

                if (dbChore.User.Id == GetUserId())
                {
                    _context.Chores.Update(dbChore);
                    await _context.SaveChangesAsync();

                    response = _mapper.Map<GetChoreDto>(dbChore);
                    return response;
                }
                else return null;
            }
            else return null;

            
        }


        private Guid GetUserId() => Guid.Parse(
    _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<GetChoreDto> GetCurrentChore()
        {
            var response = new GetChoreDto();

            var dbChore = await _context.Chores
                .Include(c => c.TimeBlocks)
                .Include(c => c.CurrentTimeBlockId)
                .FirstOrDefaultAsync(c => c.User.Id == GetUserId() && c.IsCurrentChore ==true);

            if (dbChore != null)
            {
                response = _mapper.Map<GetChoreDto>(dbChore);
                return response;
            }
            else return null;
        }

    }
}
