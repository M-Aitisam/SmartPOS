using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class BusinessCategory
    {
        [Key] 
        public int CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public List<BusinessSubItem> SubItems { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}
