using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Services;
using Microsoft.AspNetCore.Mvc;

namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("concilliation")]
public class ConcilliationController(ConcilliationService concilliationService) : ControllerBase
{

    private readonly ConcilliationService _concilliation_service = concilliationService;

    [HttpPost]
    public async Task<IActionResult> PostConcilliation([FromBody] ConcilliationDTO dto)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;
        string[] token;

        if (authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        await _concilliation_service.Compare(dto, token[1]);
        return Ok();
    }
}