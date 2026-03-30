using EscalaApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EscalaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantesController : ControllerBase
{
    private readonly IParticipanteService _service;

    public ParticipantesController(IParticipanteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetParticipantes()
    {
          var participantes = await _service.GetAllAsync();
       
        return Ok(participantes);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateParticipante(CreateParticipanteDto dto)
    {
    
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetParticipante), new { id = result.Id }, result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetParticipante(int id)
    {
        var participante = await _service.GetByIdAsync(id);
        if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }

        return Ok(participante);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParticipante(int id, UpdateParticipanteDto dto)
    {
        var participante = await _service.UpdateAsync(id, dto);
         if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }
      
        return NoContent();
    }

    [HttpDelete("{id}")]
public async Task<IActionResult> DeleteParticipante(int id)
{
    var deletado = await _service.DeleteAsync(id);

    if (!deletado)
        return NotFound("Participante não encontrado ou já inativo.");

    return NoContent();
}
}