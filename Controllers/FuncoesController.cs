using EscalaApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using EscalaApi.Domain.Entities;
using EscalaApi.DTOs;
using Microsoft.EntityFrameworkCore;
using EscalaApi.Services;

[ApiController]
[Route("api/[controller]")]
public class FuncoesController : ControllerBase
{
    private readonly IFuncaoService _funcaoService;


    public FuncoesController(IFuncaoService funcaoService)
    {
        _funcaoService = funcaoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFuncoes()
    {
        var funcoes = await _funcaoService.GetAllAsync();
       
        return Ok(funcoes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFuncao(int id)
    {
        var funcao = await _funcaoService.GetByIdAsync(id);
        if (funcao == null)
        {
            return NotFound("Função não encontrada.");
        }

        return Ok(funcao);
    }
    
    [HttpPost]

    public async Task<IActionResult> CreateFuncoes(CreateFuncaoDto dto){

            var funcao = await _funcaoService.CreateAsync(dto);
    
        return CreatedAtAction(nameof(GetFuncoes), new { id = funcao.Id }, funcao);
    }

    [HttpPut("{id}")]
    public  async Task<IActionResult> UpdateFuncao([FromRoute] int id, [FromBody] CreateFuncaoDto dto){

        var funcao = await _funcaoService.UpdateAsync(id, dto);
        if(funcao == null){
            return NotFound($"Função com id {id} não encontrada.");
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task <IActionResult> DeleteFuncao(int id)
    {
        var delete = await _funcaoService.DeleteAsync(id);
        
        if (!delete)
        {
            return NotFound($"Função com id {id} não encontrada.");
        }
    
        
        return NoContent();
    }
}