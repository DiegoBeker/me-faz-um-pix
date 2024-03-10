using me_faz_um_pix.Dtos;
using me_faz_um_pix.Models;

namespace me_faz_um_pix.Views;
public class KeyView(PixKey key, User user, PaymentProvider pp, PaymentProviderAccount ppa)
{
    public KeyDto Key { get; set; } = new KeyDto{Value = key.Value, Type = key.PixType};
    public UserView User { get; set; } = new UserView(user);
    public AccountView Account { get; set; } = new AccountView(ppa,pp);

}