using System.ComponentModel.DataAnnotations;

namespace me_faz_um_pix.Dtos;

public class UserDto
{
    [Required]
    public required string Cpf { get; set; }
}