using Microsoft.EntityFrameworkCore;
using EscalaApi.Domain.Entities;

namespace EscalaApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Participante> Participantes => Set<Participante>();
    public DbSet<Funcao> Funcoes => Set<Funcao>();
    public DbSet<FuncaoFixa> FuncoesFixas => Set<FuncaoFixa>();
}