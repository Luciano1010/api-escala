namespace EscalaApi.Domain.Entities;

public class FuncaoFixa
{
    public int Id { get; set; }
    public int FuncaoId { get; set; } 
    public Funcao Funcao{ get; set; } 

    public int ParticipanteId { get; set; }
    public Participante Participante { get; set; }

    public DayOfWeek DiaSemana { get; set; }
}