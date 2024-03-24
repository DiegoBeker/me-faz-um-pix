using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Repositories;

namespace me_faz_um_pix.Services;
public class ConcilliationService(
  ConcilliationPublishService messageService,
  PaymentProviderRepository paymentProviderRepository
)
{
    private readonly ConcilliationPublishService _messageService = messageService;
    private readonly PaymentProviderRepository _paymentProviderRepository = paymentProviderRepository;

    public async Task Compare(ConcilliationDTO dto, string token)
    {

        PaymentProvider? paymentProvider = await _paymentProviderRepository.GetByToken(token);
        if (paymentProvider == null) throw new UnauthorizedException("Invalid Token.");

        _messageService.SendMessage(dto, paymentProvider.Id);
    }
}