using OrdemServico.Domain.Entities;

namespace OrdemServico.Infrastructure.Services;

public interface IMessagePublisher
{
    Task PublishAsync(string queue, ChamadoTecnico chamado);
}