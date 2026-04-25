using OrdemServico.Domain.Entities;

namespace OrdemServico.Infrastructure.Repositories;

public interface IChamadoRepository
{
    Task<ChamadoTecnico?> GetByIdAsync(Guid id);
    Task<List<ChamadoTecnico>> GetAllAsync();
    Task<List<ChamadoTecnico>> GetByStatusAsync(int status);
    Task<List<ChamadoTecnico>> GetByClienteAsync(string cliente);
    Task AddAsync(ChamadoTecnico chamado);
    Task UpdateAsync(ChamadoTecnico chamado);
}