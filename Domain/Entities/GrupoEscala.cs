namespace EscalaApi.Domain.Entities;

public class GrupoEscala
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
   
   public List<Funcao> Funcoes { get; set; } = new();
   public List<Participante> Participantes {get; set;} = new();

   public List<EscalaItem> EscalaItems { get; set; } = new();
}