﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA.SharedDtos.Dtos.Chore
{
    public class AddChoreDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
