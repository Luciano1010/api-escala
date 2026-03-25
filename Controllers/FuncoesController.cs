using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FuncoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FuncoesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFuncoes()
    {
        var funcoes = await _context.Funcoes
        .Where(f => f.Ativo)
        .Select(f => new FuncaoResponseDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Descricao = f.Descricao,
            Ativo = f.Ativo
        }).ToListAsync();

        return Ok(funcoes);
    }

    [HttpPost]

    public async Task<IActionResult> CreateFuncoes(CreateFuncaoDto dto){

            var funcao = new Funcao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Ativo = true
        };
        _context.Funcoes.Add(funcao);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFuncoes), new { id = funcao.Id }, funcao);
    }

    [HttpPut("{id}")]
    public  async Task<IActionResult> UpdateFuncao(int id, UpdateFuncaoDto dto){

        var funcao = await _context.Funcoes.FirstOrDefaultAsync(f => f.Id == id);
        if(funcao == null){
            return NotFound($"Função com id {id} não encontrada.");
        }
        funcao.Nome = dto.Nome;
        funcao.Descricao = dto.Descricao;   
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task <IActionResult> DeleteFuncao(int id)
    {
        var funcao = await _context.Funcoes.FirstOrDefaultAsync(f => f.Id == id);
        if(funcao == null)
        {
            return NotFound($"Função com id {id}  não encontrada.");
        }
        if(!funcao.Ativo)
        {
            return NoContent();
        }
        funcao.Ativo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}