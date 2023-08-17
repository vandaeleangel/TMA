using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Mobile.Domain.Dtos.TimeBlock
{
    public class TimeBlockQueryParametersDto
    {
        public Guid? ChoreId { get; set; }
        public DateTime? Date { get; set; }
    }
}
