namespace EscalaApi.Services;
using EscalaApi.DTOs;
using EscalaApi.Domain.Entities;
using EscalaApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class FuncaoService : IFuncaoService
{
    private readonly AppDbContext _context;

    public FuncaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FuncaoResponseDto>> GetAllAsync()
    {
        return await _context.Funcoes
            .Where(f => f.Ativo)
            .Select(f => new FuncaoResponseDto
            {
                Id = f.Id,
                Nome = f.Nome,
                Descricao = f.Descricao
            })
            .ToListAsync();
    }

    public async Task<FuncaoResponseDto?> GetByIdAsync(int id)
    {
        var funcao = await _context.Funcoes.FindAsync(id);
        if (funcao == null || !funcao.Ativo)
            return null;

        return new FuncaoResponseDto
        {
            Id = funcao.Id,
            Nome = funcao.Nome,
            Descricao = funcao.Descricao
        };
    }

    public async Task<FuncaoResponseDto> CreateAsync(CreateFuncaoDto dto)
    {
        var funcao = new Funcao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        _context.Funcoes.Add(funcao);
        await _context.SaveChangesAsync();

        return new FuncaoResponseDto
        {
            Id = funcao.Id,
            Nome = funcao.Nome,
            Descricao = funcao.Descricao
        };
    }

    public async Task<FuncaoResponseDto?> UpdateAsync(int id, CreateFuncaoDto dto)
    {
        var funcao = await _context.Funcoes.FindAsync(id);
        if (funcao == null || !funcao.Ativo)
            return null;

        funcao.Nome = dto.Nome;
        funcao.Descricao = dto.Descricao;

        await _context.SaveChangesAsync();

        return new FuncaoResponseDto
        {
            Id = funcao.Id,
            Nome = funcao.Nome,
            Descricao = funcao.Descricao
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var funcao = await _context.Funcoes.FindAsync(id);
        if (funcao == null || !funcao.Ativo)
            return false;

        funcao.Ativo = false;
        await _context.SaveChangesAsync();
        return true;
    }
    }