using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class BusinessCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string? UrduName { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public int TotalProducts { get; set; }

        public List<BusinessSubItem> SubItems { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}