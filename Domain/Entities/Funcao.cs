namespace EscalaApi.Domain.Entities;

public class Funcao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public ICollection<FuncaoFixa> FuncoesFixas { get; set; }
}