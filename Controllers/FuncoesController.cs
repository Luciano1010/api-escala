using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;


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
    public IActionResult GetFuncoes()
    {
        var funcoes = _context.Funcoes
        .Where(f => f.Ativo)
        .Select(f => new FuncaoResponseDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Descricao = f.Descricao,
            Ativo = f.Ativo
        }).ToList();

        return Ok(funcoes);
    }

    [HttpPost]

    public IActionResult CreateFuncoes(CreateFuncaoDto dto){

            var funcao = new Funcao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Ativo = true
        };
        _context.Funcoes.Add(funcao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFuncoes), new { id = funcao.Id }, funcao);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFuncao(int id, UpdateFuncaoDto dto){

        var funcao = _context.Funcoes.FirstOrDefault(f => f.Id == id);
        if(funcao == null)        {
            return NotFound($"Função com id {id} não encontrada.");
        }
        funcao.Nome = dto.Nome;
        funcao.Descricao = dto.Descricao;   
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFuncao(int id)
    {
        var funcao = _context.Funcoes.FirstOrDefault(f => f.Id == id);
        if(funcao == null)
        {
            return NotFound($"Função com id {id}  não encontrado.");
        }
        funcao.Ativo = false;
        _context.SaveChanges();
        return NoContent();
    }
}