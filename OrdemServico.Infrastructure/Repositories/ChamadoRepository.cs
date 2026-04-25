using Microsoft.EntityFrameworkCore;
using OrdemServico.Domain.Entities;
using OrdemServico.Infrastructure.Data;

namespace OrdemServico.Infrastructure.Repositories;

public class ChamadoRepository : IChamadoRepository
{
    private readonly AppDbContext _context;
    public ChamadoRepository(AppDbContext context) => _context = context;
    public async Task<ChamadoTecnico?> GetByIdAsync(Guid id)
          => await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<List<ChamadoTecnico>> GetAllAsync()
          => await _context.Chamados.ToListAsync();
    public async Task<List<ChamadoTecnico>> GetByStatusAsync(int status)
          => await _context.Chamados.Where(x => x.Status == status).ToListAsync();
    public async Task<List<ChamadoTecnico>> GetByClienteAsync(string cliente)
          => await _context.Chamados.Where(x => x.Cliente.Contains(cliente)).ToListAsync();
    public async Task AddAsync(ChamadoTecnico chamado)
    {
        await _context.Chamados.AddAsync(chamado);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(ChamadoTecnico chamado)
    {
        _context.Chamados.Update(chamado);
        await _context.SaveChangesAsync();
    }
}