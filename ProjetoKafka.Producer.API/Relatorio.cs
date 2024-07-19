namespace ProjetoKafka.Producer.API;

public class Relatorio
{
    public Relatorio(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Status = StatusRelatorio.PENDENTE;
        DataProcessamento = null;
    }

    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime? DataProcessamento { get; set; }
}

public static class StatusRelatorio
{
    public const string PENDENTE = "pendente";
    public const string CONCLUIDO = "concluído";
}

public record RelatorioSolicitado(string Nome);


