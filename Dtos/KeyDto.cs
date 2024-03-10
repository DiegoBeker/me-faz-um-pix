using System.ComponentModel.DataAnnotations;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;

namespace me_faz_um_pix.Dtos;

public class KeyDto
{
    [Required]
    public required string Value { get; set; }
    [Required]
    public required string Type { get; set; }

    public PixKey ToEntity()
    {
        return new PixKey(Value, Type);
    }


}