using me_faz_um_pix.Models;

namespace me_faz_um_pix.Views;

public class UserView(User user)
{
    public string Name { get; set; } = user.Name;

    public string MaskedCpf { get; set; } = $"{user.Cpf[..3]}******{user.Cpf[^2..]}";

}