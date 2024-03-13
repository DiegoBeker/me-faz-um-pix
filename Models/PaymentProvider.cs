using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class PaymentProvider
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Token { get; set; }

    public string Name { get; set; }

    public List<PaymentProviderAccount>? PaymentProviderAccounts;

    public PaymentProvider(string token, string name)
    {
        Token = token;
        Name = name;
    }

}