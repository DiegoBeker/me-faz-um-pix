using System.ComponentModel.DataAnnotations;
using me_faz_um_pix.Models;

namespace me_faz_um_pix.Dtos;

public class CreatePaymentDto
{
    [Required]
    public required Origin Origin { get; set; }
    [Required]
    public required Destiny Destiny { get; set; }
    [Required]
    public required int Amount { get; set; }
    public string? Description { get; set; }

    public Payment ToEntity(){
        return new Payment(Amount, Description);
    }

}

public class Destiny
{
    [Required]
    public required KeyDto Key { get; set; }
}

public class Origin
{
    [Required]
    public required UserDto User { get; set; }
    [Required]
    public required AccountDto Account { get; set; }
}