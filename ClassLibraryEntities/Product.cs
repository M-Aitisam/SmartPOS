using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Add these relationships
        public int CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? NestedSubCategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public BusinessCategory Category { get; set; }

        [ForeignKey("SubCategoryID")]
        public BusinessSubItem SubCategory { get; set; }
    }
}
