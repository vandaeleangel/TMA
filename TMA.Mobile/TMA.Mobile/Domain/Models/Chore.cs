using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Models
{
    public class Chore
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public ICollection<TimeBlock> TimeBlocks { get; set; }
        public User User { get; set; }
        public Guid CurrentTimeBlockId { get; set; }
        public bool IsCurrentChore { get; set; }
        
    }
}
