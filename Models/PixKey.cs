using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class PixKey
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Value { get; set; }
    public string PixType { get; set; }

    public PaymentProviderAccount? PaymentProviderAccount;
    public long PaymentProviderAccountId { get; set; }

    public List<Payment>? Payments;

    public PixKey(string value, string pixType)
    {
        Value = value;
        PixType = pixType;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
