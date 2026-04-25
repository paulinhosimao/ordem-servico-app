using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdemServico.API.Dtos;
using OrdemServico.Domain.Entities;
using OrdemServico.Infrastructure.Repositories;
using OrdemServico.Infrastructure.Services;

namespace OrdemServico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoRepository _repository;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<ChamadosController> _logger;
    public ChamadosController(
          IChamadoRepository repository,
          IMessagePublisher publisher,
          ILogger<ChamadosController> logger)
    {
        _repository = repository;
        _publisher = publisher;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateChamadoDto dto)
    {
        var chamado = new ChamadoTecnico
        {
            Id = Guid.NewGuid(),
            Cliente = dto.Cliente,
            Descricao = dto.Descricao,
            Prioridade = dto.Prioridade,
            Status = 1,
            DataCriacao = DateTime.UtcNow
        };
        await _repository.AddAsync(chamado);
        _logger.LogInformation($"Chamado criado: {chamado.Id}");
        return CreatedAtAction(nameof(GetById), new { id = chamado.Id }, chamado);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var chamado = await _repository.GetByIdAsync(id);
        return chamado == null ? NotFound() : Ok(chamado);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? status, [FromQuery] string? cliente)
    {
        List<ChamadoTecnico> chamados;
        if (status.HasValue)
            chamados = await _repository.GetByStatusAsync(status.Value);
        else if (!string.IsNullOrEmpty(cliente))
            chamados = await _repository.GetByClienteAsync(cliente);
        else
            chamados = await _repository.GetAllAsync();
        return Ok(chamados);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChamadoDto dto)
    {
        var chamado = await _repository.GetByIdAsync(id);
        if (chamado == null)
            return NotFound();
        chamado.Status = dto.Status;
        if (dto.Status == 3)
        {
            chamado.DataFinalizacao = DateTime.UtcNow;
            await _publisher.PublishAsync("chamados.finalizados", chamado);
            _logger.LogInformation($"Chamado finalizado: {chamado.Id}");
        }
        await _repository.UpdateAsync(chamado);
        return Ok(chamado);
    }
}