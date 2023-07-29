using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.Chore;

namespace TMA.SharedDtos.Dtos.TimeBlock
{
    public class GetTimeBlockDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid ChoreId { get; set; }
        public GetChoreDto Chore { get; set; }
    }
}
