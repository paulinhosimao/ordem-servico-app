using System.Collections.Concurrent;
using OrdemServico.Domain.Entities;

namespace OrdemServico.Infrastructure.Services;

public class InMemoryMessagePublisher : IMessagePublisher
{
    // Fila em memória (simula RabbitMQ)
    public static ConcurrentQueue<ChamadoTecnico> NotificacoesFila { get; } = new();
    public async Task PublishAsync(string queue, ChamadoTecnico chamado)
    {
        // Adiciona à fila em memória
        NotificacoesFila.Enqueue(chamado);

        // Log simulado
        Console.WriteLine($"✅ [FILA] Chamado {chamado.Id} adicionado à fila de notificações");

        await Task.CompletedTask;
    }
}
