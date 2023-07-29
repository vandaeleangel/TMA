using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA.SharedDtos.Dtos.TimeBlock
{
    public class UpdateEndTimeDto
    {
        [Required]
        public Guid TimeBlockId { get; set; }
    }
}
