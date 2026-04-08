namespace EscalaApi.Domain.Entities;

public class GrupoEscala
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public int FuncaoId { get; set; } 
    public Funcao Funcao{ get; set; } = null!;

    public List<Participante> Participantes { get; set; } = new List<Participante>();
}