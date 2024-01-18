using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Dto.EmployeeSetup
{
    public class DataTableDto : BaseTableDto
    {
        public List<EmployeeDto> Data { get; set; }
    }
}
