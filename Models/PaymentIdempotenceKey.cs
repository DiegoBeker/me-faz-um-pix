namespace me_faz_um_pix.Models;

public class PaymentIdempotenceKey(Payment payment)
{
    public long ProviderAccountId {get;}= payment.PaymentProviderAccountId;
    public long PixKeyId {get;}= payment.PixKeyId;
    public int Amount {get;} = payment.Amount;

}