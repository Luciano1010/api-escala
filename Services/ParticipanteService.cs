
using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;
using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ParticipanteService : IParticipanteService
{
    private readonly AppDbContext _context;

    public ParticipanteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParticipanteResponseDto>> GetAllAsync()
    {
    
        return await _context.Participantes
            .Where(p => p.Ativo)
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
    }

    public async Task<ParticipanteResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Participantes.
        Where(p => p.Id == id && p.Ativo)
        .Select(p => new ParticipanteResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            EhAnciao = p.EhAnciao,
            EhServo = p.EhServo,
            AprovadoMecanica = p.AprovadoMecanica,
            AprovadoEnsino = p.AprovadoEnsino,
            Ativo = p.Ativo
        }).FirstOrDefaultAsync();


    }

    public async Task<ParticipanteResponseDto> CreateAsync(CreateParticipanteDto dto)
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

    public async Task<ParticipanteResponseDto?> UpdateAsync(int id, UpdateParticipanteDto dto)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Id == id);
        if (participante == null)
        {
            return null;
        }
        if (!participante.Ativo)
        {
            throw new InvalidOperationException("Não é possível atualizar um participante inativo.");
        }
        participante.Nome = dto.Nome;
        participante.EhAnciao = dto.EhAnciao;
        participante.EhServo = dto.EhServo;
        participante.AprovadoMecanica = dto.AprovadoMecanica;
        participante.AprovadoEnsino = dto.AprovadoEnsino;
        await _context.SaveChangesAsync();

        return new ParticipanteResponseDto
        {
            Id = participante.Id,
            Nome = participante.Nome,
            EhAnciao = participante.EhAnciao,
            EhServo = participante.EhServo,
            AprovadoMecanica = participante.AprovadoMecanica,
            AprovadoEnsino = participante.AprovadoEnsino,
            Ativo = participante.Ativo = true
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        if (participante == null)
        {
            return false;
        }
        participante.Ativo = false;
        await _context.SaveChangesAsync();

        return true;
    }
}