using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Dtos.TimeBlock
{
    public class AddTimeBlockDto
    {
        [Required]
        public Guid ChoreId { get; set; }
    }
}
