using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class BusinessTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public List<TransactionItem> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
    }
}
