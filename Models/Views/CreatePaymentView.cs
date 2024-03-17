using me_faz_um_pix.Dtos;

namespace me_faz_um_pix.Views;

public class CreatePaymentView(CreatePaymentDto dto)
{
    public long Id  { get; set; }

    public CreatePaymentDto Payment { get; set; }= dto;
}