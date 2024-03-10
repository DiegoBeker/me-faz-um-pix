using me_faz_um_pix.Dtos;

namespace me_faz_um_pix.Views;
public class CreateKeyView(CreateKeyDto data)
{
    public long Id  { get; set; }
    public KeyDto Key { get; set; } = data.Key;
    public UserDto User { get; set; } = data.User;
    public AccountDto Account { get; set; } = data.Account;

}