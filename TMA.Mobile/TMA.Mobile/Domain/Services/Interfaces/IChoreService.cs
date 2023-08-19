using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.Chore;
using TMA.Mobile.Domain.Models;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IChoreService
    {
        Task<IEnumerable<Chore>> GetAllChores();
        Task<Chore> AddNewChore(AddChoreDto newChore);
    }
}
