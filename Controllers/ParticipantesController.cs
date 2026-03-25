using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;
using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscalaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ParticipantesController(AppDbContext context)
    {
        _context = context;
    }

    private ParticipanteResponseDto MapToDto(Participante participante)
    {
        return new ParticipanteResponseDto
        {
            Id = participante.Id,
            Nome = participante.Nome,
            EhAnciao = participante.EhAnciao,
            EhServo = participante.EhServo,
            AprovadoMecanica = participante.AprovadoMecanica,
            AprovadoEnsino = participante.AprovadoEnsino,
            Ativo = participante.Ativo
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetParticipantes()
    {
          var participantes = await _context.Participantes.
          Where(p => p.Ativo)
          .Select(p => new ParticipanteResponseDto
          {
                Id = p.Id,
                Nome = p.Nome,
                EhAnciao = p.EhAnciao,
                EhServo = p.EhServo,
                AprovadoMecanica = p.AprovadoMecanica,
                AprovadoEnsino = p.AprovadoEnsino,
                Ativo = p.Ativo
          })
          .ToListAsync();

        return Ok(participantes);
    }
    [HttpPost]
    public async Task<IActionResult> CreateParticipante(CreateParticipanteDto dto)
    {
        var participante = new Participante
        {
            Nome = dto.Nome,
            EhAnciao = dto.EhAnciao,
            EhServo = dto.EhServo,
            AprovadoMecanica = dto.AprovadoMecanica,
            AprovadoEnsino = dto.AprovadoEnsino,
            Ativo = true
        };
        
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();
        return Ok($"Participante '{participante.Nome}' criado com sucesso!");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetParticipante(int id)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }

        var response = MapToDto(participante);
      

        return Ok(response);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParticipante(int id, CreateParticipanteDto dto)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Id == id);
        if (participante == null)
        {
            return NotFound("Participante não encontrada.");
        }
        if (!participante.Ativo)
        {
            return BadRequest("Não é possível atualizar um participante inativo.");
        }

        participante.Nome = dto.Nome;
        participante.EhAnciao = dto.EhAnciao;
        participante.EhServo = dto.EhServo;
        participante.AprovadoMecanica = dto.AprovadoMecanica;
        participante.AprovadoEnsino = dto.AprovadoEnsino;

        var response = MapToDto(participante);

        await _context.SaveChangesAsync();

        return Ok($"Participante '{response.Nome}' atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Id == id);
        if (participante == null)
        {
            return NotFound("Participante não encontrada.");
        }
        if (!participante.Ativo)
        {
            return NoContent();
        }

        participante.Ativo = false;
        await _context.SaveChangesAsync();


        return Ok($"Participante '{participante.Nome}' excluído com sucesso!");
    }

}