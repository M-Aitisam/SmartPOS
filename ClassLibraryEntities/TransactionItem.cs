using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class TransactionItem
    {
        [Key]
        public int TransactionItemID { get; set; }
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
