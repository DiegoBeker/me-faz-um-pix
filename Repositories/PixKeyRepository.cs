using me_faz_um_pix.Data;
using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Repositories;

public class PixKeyRepository
{
    public PixKeyRepository(AppDBContext context)
    {
        _context = context;
    }

    private readonly AppDBContext _context;

    public async Task<PixKey> CreatePixKey(PixKey pixKey)
    {
        _context.PixKey.Add(pixKey);
        await _context.SaveChangesAsync();
        return pixKey;
    }

    public async Task<List<PixKey>> GetAllKeysFromUser(long userId)
    {
        List<PixKey> keys = await _context.PixKey
        .Include(key => key.PaymentProviderAccount)
            .ThenInclude(account => account.User)
        .Where(key => key.PaymentProviderAccount.UserId == userId)
        .ToListAsync();

        return keys;
    }

    public async Task<List<PixKey>> GetAllKeysFromPspByUser(long userId, long ppId)
    {
        List<PixKey> keys = await _context.PixKey
        .Include(key => key.PaymentProviderAccount)
            .ThenInclude(account => account.User)
        .Where(key => key.PaymentProviderAccount.UserId == userId && key.PaymentProviderAccount.PaymentProviderId == ppId)
        .ToListAsync();

        foreach (var key in keys)
        {
            Console.WriteLine($"Id: {key.Id}, Valor: {key.Value}, Tipo: {key.PixType}, Psp: {key.PaymentProviderAccountId}");
        }

        return keys;
    }

    public async Task<PixKey?> GetKeyByValue(string value){
        return await _context.PixKey
            .FirstOrDefaultAsync(e => e.Value.Equals(value));
    }
}