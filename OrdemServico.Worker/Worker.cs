using System.Collections.Concurrent;
using OrdemServico.Domain.Entities;
using OrdemServico.Infrastructure.Services;

namespace OrdemServico.Worker;

public class ChamadoWorker : BackgroundService
{
    private readonly ILogger<ChamadoWorker> _logger;

    // Simulação de MongoDB em memória
    private static readonly ConcurrentBag<dynamic> NotificacoesMongo = new();
    public ChamadoWorker(ILogger<ChamadoWorker> logger)
    {
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker iniciado - Processando fila de notificações");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Verificar se há mensagens na fila
                if (InMemoryMessagePublisher.NotificacoesFila.TryDequeue(out var chamado))
                {
                    _logger.LogInformation($"📨 Processando chamado finalizado: {chamado.Id}");
                    _logger.LogInformation($"   Cliente: {chamado.Cliente}");
                    _logger.LogInformation($"   Descrição: {chamado.Descricao}");
                    _logger.LogInformation($"   Data Finalização: {chamado.DataFinalizacao}");
                    // Simular salvamento em MongoDB
                    var notificacao = new
                    {
                        chamadoId = chamado.Id,
                        cliente = chamado.Cliente,
                        descricao = chamado.Descricao,
                        dataFinalizacao = chamado.DataFinalizacao,
                        dataProcesamento = DateTime.UtcNow,
                        status = "Processado"
                    };
                    NotificacoesMongo.Add(notificacao);
                    _logger.LogInformation($"✅ Notificação salva em 'MongoDB' (memória). Total: {NotificacoesMongo.Count}");
                }
                // Aguardar 2 segundos antes de verificar novamente
                await Task.Delay(2000, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Erro ao processar: {ex.Message}");
            }
        }
        _logger.LogInformation("Worker parado");
    }
}

