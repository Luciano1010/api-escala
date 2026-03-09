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

}


