using me_faz_um_pix.Data;
using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Payment> GetPaymentByAccountAndKey(PaymentIdempotenceKey key, int tolerance){
        DateTime secondsAgo = DateTime.UtcNow.AddSeconds(-30);
        
        Payment? payment = await _context.Payment.Where(e => 
            e.PixKeyId.Equals(key.PixKeyId) &&
            e.PaymentProviderAccountId.Equals(key.ProviderAccountId) &&
            e.Amount.Equals(key.Amount) &&
            e.CreatedAt >= secondsAgo
        ).FirstOrDefaultAsync();
        
        return payment;
    }

    public async Task<Payment?> UpdatePaymentStatus(PaymentStatus status, long paymentId){
        
        var payment = await _context.Payment
            .Where(e => e.Id.Equals(paymentId))
            .FirstOrDefaultAsync();

        if (payment != null)
        {
            payment.Status = status; // Supondo que 'Status' é a propriedade a ser atualizada
            await _context.SaveChangesAsync();
        }

        return payment;
    }
    
}

