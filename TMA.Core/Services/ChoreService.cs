﻿using AutoMapper;
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

        public Task<GetChoreDto> DeleteChore(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetChoreDto>> GetAllChores()
        {
            throw new NotImplementedException();
        }

        public Task<GetChoreDto> GetChoreById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetChoreDto> UpdateChore(UpdateChoreDto chore)
        {
            throw new NotImplementedException();
        }


        private Guid GetUserId() => Guid.Parse(
    _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        //private Guid GetId()
        //{
        //    ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
        //    Claim nameIdentifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        //    Guid id = Guid.Parse(nameIdentifierClaim.Value);
        //    return id;
        //}

    }
}
