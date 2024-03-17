using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Services;
using me_faz_um_pix.Views;
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
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto createPaymentDto)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;
        string[] token;

        if (authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        Payment payment = await _paymentService.CreatePayment(createPaymentDto, token[1]);

        CreatePaymentView response = new(createPaymentDto)
        {
            Id = payment.Id
        };

        return Ok(response);
    }

    [HttpPatch("{status}/{paymentId}")]
    public async Task<IActionResult> UpdatePaymentStatus(string status, string paymentId)
    {
        if (!Enum.TryParse(status, true, out PaymentStatus paymentStatus) || !long.TryParse(paymentId, out long paymentIdParsed))
        {
            return BadRequest("Invalid Status");
        }

        await _paymentService.UpdatePaymentStatus(paymentStatus, paymentIdParsed);

        return NoContent();
    }
}