using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class ApiSettings
    {

        public string FilesUploadPath { get; set; }
        public string UIAppLink { get; set; }
        public string AngularAppLink { get; set; }
        public string APIAppLink { get; set; }
        public string Email { get; set; }   
        public int MemoryCacheExpirationMinutes { get; set; }
    }
}
