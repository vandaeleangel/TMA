using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Dtos.Chore
{
    public class AddChoreDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
