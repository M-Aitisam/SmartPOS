using ClassLibraryEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public bool IsPercentage { get; set; } = true;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int BusinessModelId { get; set; }
    public int BusinessDetailsId { get; set; }  // Add this
    public string BusinessTransactionId { get; set; } = string.Empty;  // Add this

    [ForeignKey("BusinessModelId")]
    public BusinessModel? BusinessModel { get; set; }

    [ForeignKey("BusinessDetailsId")]
    public BusinessDetails? BusinessDetails { get; set; }

    [ForeignKey("BusinessTransactionId")]
    public BusinessTransaction? BusinessTransaction { get; set; }
}