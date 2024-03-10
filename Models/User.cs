using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Cpf { get; set; }

    public string Name { get; set; }

    public List<PaymentProviderAccount>? PaymentProviderAccounts;

    public User(string cpf, string name)
    {
        Cpf = cpf;
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

}