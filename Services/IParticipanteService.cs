using EscalaApi.DTOs;


public interface IParticipanteService
{
    Task<List<ParticipanteResponseDto>> GetAllAsync();
    Task<ParticipanteResponseDto?> GetByIdAsync(int id);
    Task<ParticipanteResponseDto> CreateAsync(CreateParticipanteDto dto);
    Task<ParticipanteResponseDto?> UpdateAsync(int id, UpdateParticipanteDto dto);
    Task<bool> DeleteAsync(int id);

    Task ProcessarUploadAsync(IFormFile file);
} 