namespace EscalaApi.DTOs;

public class ParticipanteResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public bool EhAnciao { get; set; }
    public bool EhServo { get; set; }
    public bool AprovadoMecanica { get; set; }
    public bool AprovadoEnsino { get; set; }
    public bool Ativo { get; set; }
}