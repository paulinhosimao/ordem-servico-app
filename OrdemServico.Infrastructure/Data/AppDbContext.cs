using Microsoft.EntityFrameworkCore;
using OrdemServico.Domain.Entities;

namespace OrdemServico.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<ChamadoTecnico> Chamados { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChamadoTecnico>().HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }
}