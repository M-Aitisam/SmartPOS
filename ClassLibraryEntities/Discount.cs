using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; } = true;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Remove these conflicting properties:
        // public string? Business { get; set; }
        // public int BusinessId { get; set; }

        // Keep only one reference to BusinessModel
        public int BusinessModelId { get; set; }

        [ForeignKey("BusinessModelId")]
        public BusinessModel? BusinessModel { get; set; }
    }
}
