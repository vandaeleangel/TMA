using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.Chore;

namespace TMA.SharedDtos.Dtos.TimeBlock
{
    public class UpdateTimeBlockDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public Guid ChoreId { get; set; }


    }
}
