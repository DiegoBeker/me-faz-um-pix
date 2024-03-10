using me_faz_um_pix.Data;
using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Repositories;

public class PaymentProviderRepository
{
    public PaymentProviderRepository(AppDBContext context)
    {
        _context = context;
    }

    private readonly AppDBContext _context;

    public async Task<PaymentProvider?> GetByToken(string token)
    {
        return await _context.PaymentProvider.FirstOrDefaultAsync(e => e.Token.Equals(token));
    }
}