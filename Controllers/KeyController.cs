using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;
using me_faz_um_pix.Dtos;
using me_faz_um_pix.Exceptions;
using me_faz_um_pix.Models;
using me_faz_um_pix.Views;
using me_faz_um_pix.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
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

        if (authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        PixKey newPixKey = await _keyService.CreateKey(createKeyDto, token[1]);
        var response = new CreateKeyView(createKeyDto)
        {
            Id = newPixKey.Id
        };
        return CreatedAtAction(null, null, response);
    }

    [HttpGet("/{type}/{value}")]
    public async Task<IActionResult> GetKeyByValue(string type, string value)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;
        string[] token;

        if (authorizationHeader == null) throw new UnauthorizedException("Invalid token");
        else token = authorizationHeader.Split(' ');

        if(!ValidatePixType.ValidateType(type))
            throw new NotFoundException("Invalid Type");
        
        var response = await _keyService.GetKeyByValue(value, token[1]);

        return Ok(response);
    }

}