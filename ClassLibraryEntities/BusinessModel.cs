using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class BusinessModel
    {
        public string? BusinessID { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessType { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Location { get; set; }
        public string? CreateAt { get; set; }
        public bool IsActive { get; set; }
    }
}
