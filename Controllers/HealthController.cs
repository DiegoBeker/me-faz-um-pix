using Microsoft.AspNetCore.Mvc;
using me_faz_um_pix.Services;

namespace me_faz_um_pix.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
  private readonly HealthService _healthService;

  public HealthController(HealthService healthService)
  {
    _healthService = healthService;
  }
	
	[HttpGet]
  public IActionResult GetHealth()
  {
    String message = _healthService.GetHealthMessage();
		return Ok(message);
  }
}