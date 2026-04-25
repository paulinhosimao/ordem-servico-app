namespace OrdemServico.API.Dtos;

public class CreateChamadoDto
{
    public string Cliente { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Prioridade { get; set; }
}