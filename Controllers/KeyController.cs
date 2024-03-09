using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;
using Microsoft.AspNetCore.Authorization;

namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("keys")]
public class KeyController : ControllerBase
{
    private readonly KeyService _keyService;
    private readonly TokenService _tokenService;

    public KeyController(KeyService keyService, TokenService tokenService)
    {
        _keyService = keyService;
        _tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult GetAllKeys()
    {
        String token = _tokenService.GenerateToken(3);
        return Ok(token);
    }

}