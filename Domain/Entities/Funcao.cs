using EscalaApi.Domain.Entities;

public class Funcao
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public int QuantidadePessoas { get; set; }

    public bool Ativo { get; set; } = true;

    public TipoExecucao TipoExecucao { get; set; } // ← IMPORTANTE

    public int GrupoEscalaId { get; set; }
    public GrupoEscala Grupo { get; set; } = null!;

    public List<ParticipanteFuncao> ParticipanteFuncoes { get; set; } = new();
}