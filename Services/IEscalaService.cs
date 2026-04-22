using EscalaApi.Domain.Entities;
namespace EscalaApi.Services;


public interface IEscalaService
{
    GrupoEscala GerarEscala(int escalaId);
}