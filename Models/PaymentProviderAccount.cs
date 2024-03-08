using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class PaymentProviderAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Agency { get; set; } 
    public string Number { get; set; }

    public User User {get; set ;}
    public long UserId {get; set ;}

}