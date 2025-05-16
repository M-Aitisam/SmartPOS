namespace ClassLibraryEntities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SubTotal { get; set; }
     
        public bool IsReturn { get; set; }
        public List<TransactionItem> Items { get; set; } = new List<TransactionItem>();
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string? PaymentMethod { get; set; }

    }
}
