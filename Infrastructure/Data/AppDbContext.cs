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
    public DbSet<ParticipanteFuncao> ParticipanteFuncaos => Set<ParticipanteFuncao>();
    public DbSet<GrupoEscala> GruposEscala => Set<GrupoEscala>();
    public DbSet<ParticipanteGrupo> ParticipanteGrupos => Set<ParticipanteGrupo>();
    public DbSet<Escala> Escalas => Set<Escala>();
    public DbSet<EscalaItem> EscalaItems => Set<EscalaItem>();


}