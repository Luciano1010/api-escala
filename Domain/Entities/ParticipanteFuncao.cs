using EscalaApi.Domain.Entities;

public class ParticipanteFuncao
{
    public int Id { get; set; }

    public int ParticipanteId { get; set; }
    public Participante Participante { get; set; } = null!;

    public int FuncaoId { get; set; }
    public Funcao Funcao { get; set; } = null!;
}