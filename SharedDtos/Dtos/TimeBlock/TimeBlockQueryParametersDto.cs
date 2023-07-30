using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA.SharedDtos.Dtos
{
    public class TimeBlockQueryParametersDto
    {
        public Guid? ChoreId { get; set; }
        public DateTime? Date { get; set; }
    }
}
