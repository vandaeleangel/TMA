using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Dtos.TimeBlock
{
    public class UpdateEndTimeDto
    {
        [Required]
        public Guid TimeBlockId { get; set; }
    }
}
