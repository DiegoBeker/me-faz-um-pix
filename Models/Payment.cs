using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public PaymentStatus Status { get; set; }

    public long PixKeyId { get; set; }

    public PixKey? PixKey;
    public long PaymentProviderAccountId { get; set; }
    public PaymentProviderAccount? PaymentProviderAccount;
    public int Amount { get; set; }
    public string? Description { get; set; }

    public Payment(int amount, string? description)
    {
        Amount = amount;
        Status = PaymentStatus.PROCESSING;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}