using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;

namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyController : ControllerBase
{
    private readonly KeyService _keyService;

    public KeyController(KeyService keyService)
    {
        _keyService = keyService;
    }

    [HttpGet]
    public IActionResult CreateKey()
    {
        String message = _keyService.CreateKey();
        return Ok(message);
    }
}