namespace EscalaApi.DTOs;

public class CreateParticipanteDto
{
    public string Nome { get; set; }
    public bool EhAnciao { get; set; }
    public bool EhServo { get; set; }
    public bool AprovadoMecanica { get; set; }
    public bool AprovadoEnsino { get; set; }
}