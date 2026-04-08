using EscalaApi.Domain.Entities;

public class ParticipanteGrupo
{
    public int Id { get; set; }

    public int ParticipanteId { get; set; }
    public Participante Participante { get; set; } = null!;

    public int GrupoEscalaId { get; set; }
    public GrupoEscala GrupoEscala { get; set; } = null!;
}