using me_faz_um_pix.Data;
using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Repositories;

public class UserRespository
{
    private readonly AppDBContext _context;
    public UserRespository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByCpfWithAccounts(string cpf)
    {
        return await _context.User
            .Where(u => u.Cpf == cpf)
            .Include(u => u.PaymentProviderAccounts)
                .ThenInclude(ppa => ppa.PaymentProvider)
            .Include(u => u.PaymentProviderAccounts)
                .ThenInclude(ppa => ppa.PixKeys)
            .FirstOrDefaultAsync();
    
    }

    public async Task<User?> GetByCpf(string cpf)
    {
        return await _context.User.FirstOrDefaultAsync(e => e.Cpf.Equals(cpf));
    }

    public async Task<User?> GetById(long id)
    {
        return await _context.User.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

}