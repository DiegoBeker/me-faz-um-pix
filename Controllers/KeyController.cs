using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    public IActionResult GetAllKeys()
    {
        string? authorizationHeader = HttpContext.Request.Headers["Authorization"];
        return Ok(authorizationHeader);
    }

}