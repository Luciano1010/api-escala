using DocumentFormat.OpenXml.InkML;
using EscalaApi.DTOs;
using EscalaApi.Infrastructure.Data;
using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EscalaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantesController : ControllerBase
{
    private readonly IParticipanteService _service;
    private readonly ILeitorArquivo _leitorArquivo;

    private readonly AppDbContext _context;

    public ParticipantesController(IParticipanteService service, ILeitorArquivo leitorArquivo, AppDbContext context)
    {
        _service = service;
        _leitorArquivo = leitorArquivo;
        _context = context;
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



    [HttpPost("upload")]
    public async Task<IActionResult> UploadParticipantes(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");
        try
        {
            var nomes = await _leitorArquivo.LerNomesAsync(file);
            await _service.SalvarParticipantesAsync(nomes);
            return Ok(nomes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar arquivo: {ex.Message}");
        }
       
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