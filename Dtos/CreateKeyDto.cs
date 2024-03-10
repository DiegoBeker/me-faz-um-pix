using System.ComponentModel.DataAnnotations;

namespace me_faz_um_pix.Dtos;

public class CreateKeyDto
{
    [Required]
    public required KeyDto Key { get; set; }
    [Required]
    public required UserDto User { get; set; }
    [Required]
    public required AccountDto Account { get; set; }

}