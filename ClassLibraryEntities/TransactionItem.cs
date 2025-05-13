using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        // Remove this line: public string? TransactionID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public string TransactionId { get; set; } = string.Empty; // Correct foreign key
        public BusinessTransaction? Transaction { get; set; }
    }
}