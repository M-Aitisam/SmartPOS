using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class BusinessTransaction
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int TransactionID { get; set; }
        public List<TransactionItem> Items { get; set; } = new();
        public string? CustomerName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "Cash";
        public decimal AmountReceived { get; set; }
        public string? StaffName { get; set; }
        public string? TableNumber { get; set; }
        public decimal Tax { get; set; }
    }
}
