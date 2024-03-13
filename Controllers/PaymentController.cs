using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Services;
using Microsoft.AspNetCore.Mvc;

namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public IActionResult CreatePayment([FromBody] CreatePaymentDto createPaymentDto)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;
        string[] token;

        if (authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        var response = _paymentService.CreatePayment(createPaymentDto);

        return CreatedAtAction(null, null, response);
    }
}