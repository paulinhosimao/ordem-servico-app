namespace OrdemServico.Domain.Entities;

public class ChamadoTecnico
{
    public Guid Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Prioridade { get; set; } // 1=Baixa, 2=Média, 3=Alta
    public int Status { get; set; } // 1=Aberto, 2=EmAndamento, 3=Finalizado
    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }
}