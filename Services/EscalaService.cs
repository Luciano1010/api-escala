using EscalaApi.Domain.Entities;
namespace EscalaApi.Services;

public class EscalaService : IEscalaService
{

  public GrupoEscala GerarEscala(int escalaId)
  {

    var escala = new GrupoEscala();

    var participantes = new List<Participante>();

    var p1 = new Participante { Nome = "Wesley" };
    var p2 = new Participante { Nome = "Ryan" };
    var p3 = new Participante { Nome = "MIchael" };

    participantes.Add(p1);
    participantes.Add(p2);
    participantes.Add(p3);

    escala.Participantes = participantes;

    var funcoes = new List<Funcao>();

    var f1 = new Funcao { Nome = "Microfone Volante" };
    var f2 = new Funcao { Nome = "Som" };
    var f3 = new Funcao { Nome = "Indicador" };

    funcoes.Add(f1);
    funcoes.Add(f2);
    funcoes.Add(f3);

    escala.Funcoes = funcoes;

    var itemManual = new EscalaItem
    {
      Funcao = f1,
      Participante = p1,
      TipoExecucao = TipoExecucao.Manual,
      Data = DateTime.Now
    };

    escala.EscalaItems.Add(itemManual);

    var disponiveis = AplicarManual(escala);

    return escala;
  }

  private List<Participante> AplicarManual(GrupoEscala escala)
  {
    var participantesDisponiveis = escala.Participantes.ToList();

    foreach (var item in escala.EscalaItems)
    {

      if (item.TipoExecucao == TipoExecucao.Manual && item.Participante != null)
      {

        participantesDisponiveis.Remove(item.Participante);
      }
    }

    return participantesDisponiveis;

  }

  private void AplicarAutomatico(GrupoEscala escala)
  {
    
  }


}