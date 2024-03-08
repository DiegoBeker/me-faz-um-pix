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
    public PixTypeEnum PixType { get; set; }

    public PaymentProviderAccount? PaymentProviderAccount { get; set; }
    public long PaymentProviderAccountId { get; set; }

    public PixKey(string value, PixTypeEnum pixType)
    {
        Value = value;
        PixType = pixType;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
