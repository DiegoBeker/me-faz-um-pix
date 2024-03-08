using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace me_faz_um_pix.Models;

public class PixKey(string value, PixTypeEnum type)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Value { get; set; } = value;
    public PixTypeEnum Type { get; set; } = type;
}
