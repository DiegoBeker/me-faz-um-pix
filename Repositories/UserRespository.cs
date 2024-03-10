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

    public async Task<User?> GetByCpf(string cpf)
    {
        return await _context.User.FirstOrDefaultAsync(e => e.Cpf.Equals(cpf));
    }

}