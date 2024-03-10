using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;
using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("keys")]
public class KeyController : ControllerBase
{
    private readonly KeyService _keyService;

    public KeyController(KeyService keyService)
    {
        _keyService = keyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateKey(CreateKeyDto createKeyDto)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;
        string[] token;

        if(authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        PixKey newPixKey = await _keyService.CreateKey(createKeyDto, token[1]);
        
        return CreatedAtAction(null,null, newPixKey);
    }

}