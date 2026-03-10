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
    public IActionResult GetParticipantes()
    {
          var participantes = _context.Participantes.Select(p => new ParticipanteResponseDto
          {
                Id = p.Id,
                Nome = p.Nome,
                EhAnciao = p.EhAnciao,
                EhServo = p.EhServo,
                AprovadoMecanica = p.AprovadoMecanica,
                AprovadoEnsino = p.AprovadoEnsino,
                Ativo = p.Ativo
          })
          .ToList();

        return Ok(participantes);
    }
    [HttpPost]
    public IActionResult CreateParticipante(CreateParticipanteDto dto)
    {
        var participante = new Participante
        {
            Nome = dto.Nome,
            EhAnciao = dto.EhAnciao,
            EhServo = dto.EhServo,
            AprovadoMecanica = dto.AprovadoMecanica,
            AprovadoEnsino = dto.AprovadoEnsino
        };
        
        _context.Participantes.Add(participante);
        _context.SaveChanges();
        return Ok($"Participante '{participante.Nome}' criado com sucesso!");
    }
    [HttpGet("{id}")]
    public IActionResult GetParticipante(int id)
    {
        var participante = _context.Participantes.FirstOrDefault(p => p.Id == id);
        if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }

        var response = MapToDto(participante);
      

        return Ok(response);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateParticipante(int id, CreateParticipanteDto dto)
    {
        var participante = _context.Participantes.FirstOrDefault(p => p.Id == id);
        if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }

        participante.Nome = dto.Nome;
        participante.EhAnciao = dto.EhAnciao;
        participante.EhServo = dto.EhServo;
        participante.AprovadoMecanica = dto.AprovadoMecanica;
        participante.AprovadoEnsino = dto.AprovadoEnsino;

        var response = MapToDto(participante);

        _context.SaveChanges();

        return Ok($"Participante '{response.Nome}' atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteParticipante(int id)
    {
        var participante = _context.Participantes.FirstOrDefault(p => p.Id == id);
        if (participante == null)
        {
            return NotFound("Participante não encontrado.");
        }

        _context.Participantes.Remove(participante);
        _context.SaveChanges();


        return Ok($"Participante '{participante.Nome}' excluído com sucesso!");
    }

}