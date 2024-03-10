using me_faz_um_pix.Models;

namespace me_faz_um_pix.Views;

public class AccountView(PaymentProviderAccount ppa, PaymentProvider pp)
{
    public string Number { get; set; } = ppa.Number;
    public string Agency { get; set; } = ppa.Agency;
    public long BankId { get; set; } = pp.Id;
    public string BankName { get; set; }= pp.Name;

}