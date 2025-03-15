using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class BusinessSubItem
    {
        public int SubItemID { get; set; }
        public string SubItemName { get; set; } = string.Empty;
        public int CategoryID { get; set; }  // Foreign key reference
    }
}
