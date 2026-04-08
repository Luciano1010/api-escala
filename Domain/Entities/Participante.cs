namespace EscalaApi.Domain.Entities;

public class Participante
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    
}