using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class BusinessSubItem
    {
        [Key]
        public int SubItemID { get; set; }
        public string SubItemName { get; set; } = string.Empty;
        public int? ParentSubItemID { get; set; }
        public List<BusinessSubItem> NestedSubItems { get; set; } = new();
        public decimal Price { get; set; }
        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }  // Foreign key reference
        public BusinessCategory? Category { get; set; }
        public BusinessSubItem? ParentSubItem { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
