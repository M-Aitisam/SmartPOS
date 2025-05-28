using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class Product
    {
        public int ProductID { get; set; }

        
        public string? ProductUrduName { get; set; }

        [Required(ErrorMessage = "Product title is required.")]
        [StringLength(100, ErrorMessage = "ProductTitle cannot exceed 100 characters.")]
        public string? ProductTitle { get; set; }

       
        public string? ProductCode { get; set; }

       
        public decimal ProductPrice { get; set; }

        public bool IsActive { get; set; } = true;

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        public string ActiveDays { get; set; } = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday";

        
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }

        public int? SubCategoryID { get; set; }
        public int? NestedSubCategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public BusinessCategory? Category { get; set; }

        [ForeignKey("SubCategoryID")]
        public BusinessSubItem? SubCategory { get; set; }

    }
}
