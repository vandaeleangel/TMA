using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.Chore;

namespace TMA.Core.Interfaces
{
    public interface IChoreService
    {
        Task<List<GetChoreDto>> GetAllChores();
        Task<GetChoreDto> GetChoreById(Guid id);
        Task<GetChoreDto> AddChore(AddChoreDto chore);
        Task<GetChoreDto> UpdateChore(UpdateChoreDto chore);
        Task<GetChoreDto> DeleteChore (Guid id);
    }
}
