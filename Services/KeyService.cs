using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Repositories;
using me_faz_um_pix.Views;

namespace me_faz_um_pix.Services;

public class KeyService
{
  private readonly PaymentProviderRepository _paymentProviderRepository;
  private readonly PaymentProviderAccountRepository _paymentProviderAccountRepository;
  private readonly PixKeyRepository _pixKeyRepository;
  private readonly UserRespository _userRepository;

  public KeyService
  (
    PaymentProviderRepository paymentProviderRepository,
    PaymentProviderAccountRepository paymentProviderAccountRepository,
    PixKeyRepository pixKeyRepositoy,
    UserRespository userRespository
  )
  {
    _paymentProviderAccountRepository = paymentProviderAccountRepository;
    _paymentProviderRepository = paymentProviderRepository;
    _pixKeyRepository = pixKeyRepositoy;
    _userRepository = userRespository;
  }
  public async Task<PixKey> CreateKey(CreateKeyDto data, string token)
  {
    PixKey? keyExists = await _pixKeyRepository.GetKeyByValue(data.Key.Value);

    if (keyExists != null) throw new ConflictException("Key Already in use");

    PaymentProvider? paymentProvider = await _paymentProviderRepository.GetByToken(token);

    if (paymentProvider == null) throw new UnauthorizedException("Invalid Token.");

    User? user = await _userRepository.GetByCpf(data.User.Cpf);

    if (user == null) throw new NotFoundException("Cpf not found.");

    if (data.Key.Type == "CPF" && (user.Cpf != data.Key.Value))
      throw new ForbiddenException("Cpf key does no match with user Cpf");

    PaymentProviderAccount? paymentProviderAccount = await _paymentProviderAccountRepository
      .GetByAgencyAndNumber(data.Account.Agency, data.Account.Number);

    if (paymentProviderAccount != null && (paymentProviderAccount.UserId != user.Id))
      throw new ForbiddenException("User cpf does not match with account user");

    if (paymentProviderAccount == null)
    {
      PaymentProviderAccount newPPA = data.Account.ToEntity();
      newPPA.PaymentProviderId = paymentProvider.Id;
      newPPA.UserId = user.Id;
      paymentProviderAccount = await _paymentProviderAccountRepository.CreateAccount(newPPA);
    }

    List<PixKey> keys = await _pixKeyRepository.GetAllKeysFromUser(user.Id);

    List<PixKey> cpfKeys = keys.Where(key => key.PixType == "CPF").ToList();

    if (data.Key.Type == "CPF" && cpfKeys.Count > 0)
      throw new ConflictException("User already have a key of type CPF");

    if (cpfKeys.Count == 20) throw new ForbiddenException("User already reached 20 keys");

    List<PixKey> cpfPspKeys = await _pixKeyRepository.GetAllKeysFromPspByUser(user.Id, paymentProvider.Id);


    if (cpfPspKeys.Count == 5) throw new ForbiddenException("User reached the limit of keys per PSP");

    PixKey newPixKey = data.Key.ToEntity();
    newPixKey.PaymentProviderAccountId = paymentProviderAccount.Id;

    PixKey result = await _pixKeyRepository.CreatePixKey(newPixKey);

    return result;
  }

  public async Task<KeyView> GetKeyByValue(string value, string token)
  {
    PaymentProvider? paymentProvider = await _paymentProviderRepository.GetByToken(token);

    if (paymentProvider == null) throw new UnauthorizedException("Invalid Token.");

    PixKey? keyExists = await _pixKeyRepository.GetKeyByValue(value);

    if (keyExists == null) throw new NotFoundException("Key not found");

    PaymentProviderAccount? ppa = await _paymentProviderAccountRepository.GetById(keyExists.PaymentProviderAccountId);

    User user = await _userRepository.GetById(ppa.UserId);

    KeyView result = new KeyView(keyExists, user, paymentProvider, ppa);

    return result;
  }
}