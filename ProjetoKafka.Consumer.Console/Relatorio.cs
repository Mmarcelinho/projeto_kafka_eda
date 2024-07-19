using System.Text.Json.Serialization;

namespace ProjetoKafka.Consumer.Console;

public class Relatorio
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    [JsonPropertyName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("DataProcessamento")]
    public DateTime? DataProcessamento { get; set; }

    public override string ToString()
    {
        return $"O Relatório {this.Nome} está com o Status: {this.Status} na Data: {this.DataProcessamento}";
    }
}

public static class StatusRelatorio
{
    public const string PENDENTE = "pendente";
    public const string CONCLUIDO = "concluído";
}



