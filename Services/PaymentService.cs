using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Repositories;
using me_faz_um_pix.Views;

namespace me_faz_um_pix.Services;

public class PaymentService(
    PaymentProviderRepository paymentProviderRepository,
    PaymentProviderAccountRepository paymentProviderAccountRepository,
    PixKeyRepository pixKeyRepositoy,
    UserRespository userRespository,
    PaymentRepository paymentRepository,
    MessageService messsageService
    )
{
    private readonly PaymentProviderRepository _paymentProviderRepository = paymentProviderRepository;
    private readonly PaymentProviderAccountRepository _paymentProviderAccountRepository = paymentProviderAccountRepository;
    private readonly PixKeyRepository _pixKeyRepository = pixKeyRepositoy;
    private readonly UserRespository _userRepository = userRespository;
    private readonly PaymentRepository _paymentRepository = paymentRepository;

    private readonly MessageService _messsageService = messsageService;
    private readonly int IDEMPOTENCY_SECONDS_TOLERANCE = 30;

    public async Task<Payment> CreatePayment(CreatePaymentDto data, string token)
    {
        Payment newPayment = await ValidatePayment(data, token);

        PaymentIdempotenceKey key = new(newPayment);

        if (await CheckIfDuplicatedIdempotenceKey(key))
            throw new RecentPaymentViolationException($"Can't accept the same payment under {IDEMPOTENCY_SECONDS_TOLERANCE}s");

        Payment payment = await _paymentRepository.CreatePayment(newPayment);

        CreatePaymentView view = new(data) { Id = payment.Id };

        DateTime now = DateTime.Now;
        string formattedDateTime = now.ToString("dd/MM/yyyy HH:mm:ss");
        ProcessPayment(view, formattedDateTime);

        return payment;
    }

    private async Task<bool> CheckIfDuplicatedIdempotenceKey(PaymentIdempotenceKey key)
    {
        Payment? payment = await _paymentRepository.GetPaymentByAccountAndKey(key, IDEMPOTENCY_SECONDS_TOLERANCE);
        return payment != null;
    }

    public void ProcessPayment(CreatePaymentView payment, string creationTime)
    {
        _messsageService.SendMessage(payment, creationTime);
    }

    public async Task<Payment?> UpdatePaymentStatus(PaymentStatus status, long paymentId)
    {
        Payment? payment = await _paymentRepository.UpdatePaymentStatus(status, paymentId);
        return payment;
    }

    public async Task<Payment> ValidatePayment(CreatePaymentDto data, string token)
    {
        PaymentProvider? paymentProvider = await _paymentProviderRepository.GetByToken(token);
        if (paymentProvider == null) throw new UnauthorizedException("Invalid Token.");

        PixKey? destinyKey = await _pixKeyRepository.GetKeyByValue(data.Destiny.Key.Value);

        if (destinyKey == null) throw new NotFoundException("Key value not found");

        PaymentProviderAccount? originAccount = await _paymentProviderAccountRepository
            .GetByAgencyAndNumber(data.Origin.Account.Agency, data.Origin.Account.Number);

        if (originAccount == null) throw new NotFoundException("Account not found");

        if (originAccount.User != null && (originAccount.User.Cpf != data.Origin.User.Cpf))
            throw new ForbiddenException("User cpf does not match with account user cpf");

        if (destinyKey.PaymentProviderAccountId == originAccount.Id)
            throw new ForbiddenException("Can't send a payment to the same account");

        Payment newPayment = data.ToEntity();
        newPayment.PaymentProviderAccountId = originAccount.Id;
        newPayment.PixKeyId = destinyKey.Id;

        return newPayment;
    }
}