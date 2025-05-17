using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class BusinessModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessID { get; set; }

        // Navigation properties for the steps
        public GeneralInformation GeneralInformation { get; set; } = new();
        public BusinessDetails BusinessDetails { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }
}