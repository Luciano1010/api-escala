using EscalaApi.Domain.Entities;

public class EscalaItem
{
    public int Id { get; set; }

    public int EscalaId { get; set; }
    public Escala Escala { get; set; } = null!;

    public int FuncaoId { get; set; }
    public Funcao Funcao { get; set; } = null!;

    public int ParticipanteId { get; set; }
    public Participante Participante { get; set; } = null!;

    public DateTime Data { get; set; }
}