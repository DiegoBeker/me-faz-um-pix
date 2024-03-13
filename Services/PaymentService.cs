using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Repositories;

namespace me_faz_um_pix.Services;

public class PaymentService
{
    private readonly PaymentProviderRepository _paymentProviderRepository;
    private readonly PaymentProviderAccountRepository _paymentProviderAccountRepository;
    private readonly PixKeyRepository _pixKeyRepository;
    private readonly UserRespository _userRepository;
    private readonly PaymentRepository _paymentRepository;

    public PaymentService(
        PixKeyRepository pixKeyRepository,
        PaymentProviderRepository paymentProviderRepository,
        PaymentProviderAccountRepository paymentProviderAccountRepository,
        PixKeyRepository pixKeyRepositoy,
        UserRespository userRespository,
        PaymentRepository paymentRepository

    )
    {
        _paymentProviderAccountRepository = paymentProviderAccountRepository;
        _paymentProviderRepository = paymentProviderRepository;
        _pixKeyRepository = pixKeyRepositoy;
        _userRepository = userRespository;
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> CreatePayment(CreatePaymentDto data, string token)
    {
        PaymentProvider? paymentProvider = await _paymentProviderRepository.GetByToken(token);
        if (paymentProvider == null) throw new UnauthorizedException("Invalid Token.");

        PixKey? keyExists = await _pixKeyRepository.GetKeyByValue(data.Destiny.Key.Value);

        if (keyExists == null) throw new NotFoundException("Key value not found");

        PaymentProviderAccount? paymentProviderAccount = await _paymentProviderAccountRepository
            .GetByAgencyAndNumber(data.Origin.Account.Agency, data.Origin.Account.Number);

        if (paymentProviderAccount == null) throw new NotFoundException("Account not found");

        User? user = await _userRepository.GetByCpf(data.Origin.User.Cpf);

        if (user == null) throw new NotFoundException("Cpf not found.");

        if (paymentProviderAccount != null && (paymentProviderAccount.UserId != user.Id))
            throw new ForbiddenException("User cpf does not match with account user cpf");
        
        Payment newPayment = data.ToEntity();
        newPayment.PaymentProviderAccountId = paymentProviderAccount.Id;
        newPayment.PixKeyId = keyExists.Id;
        
        Payment payment = await _paymentRepository.CreatePayment(newPayment);

        return payment;
    }
}