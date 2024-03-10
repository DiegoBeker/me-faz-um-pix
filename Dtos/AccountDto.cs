using System.ComponentModel.DataAnnotations;
using me_faz_um_pix.Models;

namespace me_faz_um_pix.Dtos;

public class AccountDto
{
    [Required]
    public required string Number { get; set; }
    [Required]
    public required string Agency { get; set; }

    public PaymentProviderAccount ToEntity(){
        return new PaymentProviderAccount(Agency, Number);
    }

}