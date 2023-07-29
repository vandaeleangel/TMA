using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Core.Entities;
using TMA.SharedDtos.Dtos.Chore;

namespace TMA.Core.AutoMapperProfiles
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Chore,GetChoreDto>();
            
        }
    }
}
