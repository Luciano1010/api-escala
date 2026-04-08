using EscalaApi.Domain.Entities;

public class Escala
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public DateTime DataInicio { get; set; }

    public DateTime DataFim { get; set; }

    public List<Funcao> Funcoes { get; set; } = new();

    public List<Participante> Participantes { get; set; } = new();
}