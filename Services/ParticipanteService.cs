
using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;
using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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


    public async Task CriarParticipantes(List<string> nomes)
    {
        var nomesLimpos = nomes
           .Where(n => !string.IsNullOrWhiteSpace(n))
           .Select(n => n.Trim().ToLower())
           .Distinct()
           .ToList();


        var nomesNormaizados = nomesLimpos
            .Select(n => n.Trim().ToLower())
            .ToList();

        var nomesExistentes = await _context.Participantes
            .Where(p => nomesLimpos.Contains(p.Nome.ToLower()))
            .Select(p => p.Nome.ToLower())
            .ToListAsync();

        var novos = nomesLimpos
            .Where(n => !nomesExistentes.Contains(n.ToLower()))
            .Select(nome => new Participante
            {
                Nome = nome,
                Ativo = true
            });

        await _context.Participantes.AddRangeAsync(novos);
        await _context.SaveChangesAsync();
    }
    public async Task<ParticipanteResponseDto> CreateAsync(CreateParticipanteDto dto)
    {
        await CriarParticipantes(new List<string> { dto.Nome });
        
        var nomeNormalizado = dto.Nome.Trim().ToLower();
        var participante = await _context.Participantes
        .FirstAsync(p => p.Nome.ToLower() == nomeNormalizado);

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
            Ativo = participante.Ativo
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

    public async Task SalvarParticipantesAsync(List<string> nomes)
{
    foreach (var nome in nomes)
    {
        var nomeNormalizado = nome.Trim().ToLower();

        var existe = await _context.Participantes
            .AnyAsync(p => p.Nome.ToLower() == nomeNormalizado);

        if (!existe)
        {
            var participante = new Participante
            {
                Nome = nome.Trim(),
                Ativo = true
            };

            _context.Participantes.Add(participante);
        }
    }

    await _context.SaveChangesAsync();
}
}