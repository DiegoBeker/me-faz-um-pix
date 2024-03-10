using me_faz_um_pix.Data;
using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Repositories;

public class PaymentProviderAccountRepository
{
    public PaymentProviderAccountRepository(AppDBContext context)
    {
        _context = context;
    }

    private readonly AppDBContext _context;

    public async Task<PaymentProviderAccount?> GetByAgencyAndNumber(string agency, string number )
    {
        return await _context.PaymentProviderAccount.FirstOrDefaultAsync(e => e.Agency.Equals(agency) && e.Number.Equals(number));
    }

    public async Task<PaymentProviderAccount> CreateAccount(PaymentProviderAccount paymentProviderAccount){
        _context.PaymentProviderAccount.Add(paymentProviderAccount);
        await _context.SaveChangesAsync();
        return paymentProviderAccount;
    }
}