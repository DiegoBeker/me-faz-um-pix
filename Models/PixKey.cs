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
    public PixTypeEnum Type { get; set; }

    public PaymentProviderAccount PaymentProviderAccount { get; set; }
    public long PaymentProviderAccountId { get; set; }
}
