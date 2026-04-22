using Microsoft.AspNetCore.Mvc;
using EscalaApi.Services;

namespace EscalaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscalaController : ControllerBase
{
    private readonly IEscalaService _escalaService;

    public EscalaController(IEscalaService escalaService)
    {
        _escalaService = escalaService;
    }

    [HttpGet("gerar/{id}")]
    public IActionResult Gerar(int id)
    {
        var resultado = _escalaService.GerarEscala(id);
        return Ok(resultado);
    }
}