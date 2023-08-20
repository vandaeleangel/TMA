using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Models
{
    public class TimeBlock
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }

        [Required]
        public Guid ChoreId { get; set; }

        public Chore Chore { get; set; }

    }
}
