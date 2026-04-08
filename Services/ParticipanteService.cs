
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
            Ativo = p.Ativo
        }).FirstOrDefaultAsync();


    }

    public async Task<ParticipanteResponseDto> CreateAsync(CreateParticipanteDto dto)
    {
        var participante = new Participante
        {
            Nome = dto.Nome,
            Ativo = true
        };
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();

        return new ParticipanteResponseDto
        {
            Id = participante.Id,
            Nome = participante.Nome,
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
        await _context.SaveChangesAsync();

        return new ParticipanteResponseDto
        {
            Id = participante.Id,
            Nome = participante.Nome,
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