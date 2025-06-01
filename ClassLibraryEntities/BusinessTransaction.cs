using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class BusinessTransaction
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TransactionId { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
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
        public bool IsReturn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public List<Discount> AppliedDiscounts { get; set; } = new();
    }
}
