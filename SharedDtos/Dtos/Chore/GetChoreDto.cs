using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.TimeBlock;

namespace TMA.SharedDtos.Dtos.Chore
{
    public class GetChoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public ICollection<GetTimeBlockDto> TimeBlocks { get; set; }
        public bool IsCurrentChore { get; set; } = false;
        public Guid CurrentTimeBlockId { get; set; } 
    }
}
