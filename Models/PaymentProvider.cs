using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class PaymentProvider(string token)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Token { get; set; } = token;

    public List<PaymentProviderAccount> PaymentProviderAccounts { get; } = [];

}