using EscalaApi.DTOs;
using EscalaApi.Domain.Entities;

namespace EscalaApi.DTOs;

public class UpdateParticipanteDto
{
    public string Nome { get; set; } = string.Empty;
    public bool EhAnciao { get; set; }
    public bool EhServo { get; set; }
    public bool AprovadoMecanica { get; set; }
    public bool AprovadoEnsino { get; set; }
}