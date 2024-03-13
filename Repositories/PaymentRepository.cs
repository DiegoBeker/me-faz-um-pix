using me_faz_um_pix.Data;
using me_faz_um_pix.Models;

namespace me_faz_um_pix.Repositories;

public class PaymentRepository
{
    private readonly AppDBContext _context;

    public PaymentRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Payment> CreatePayment(Payment payment)
    {
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }
    
}

