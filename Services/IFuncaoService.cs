namespace EscalaApi.Services;

using EscalaApi.DTOs;

public interface IFuncaoService
{
    Task<List<FuncaoResponseDto>> GetAllAsync();
    Task<FuncaoResponseDto?> GetByIdAsync(int id);
    Task<FuncaoResponseDto> CreateAsync(CreateFuncaoDto dto);
    Task<FuncaoResponseDto?> UpdateAsync(int id, CreateFuncaoDto dto);
    Task<bool> DeleteAsync(int id);
}