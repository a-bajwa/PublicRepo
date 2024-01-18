using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class BaseTableDto
    {
        public int TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
        public int Draw { get; set; }
    }
}
