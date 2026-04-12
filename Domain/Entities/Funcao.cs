using EscalaApi.Domain.Entities;

public class Funcao
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public int QuantidadePessoas { get; set; }

    public bool Ativo { get; set; } = true;

    public List<ParticipanteFuncao> ParticipanteFuncoes { get; set; } = new();
    public List<GrupoEscala> Grupos { get; set; } = new();
}