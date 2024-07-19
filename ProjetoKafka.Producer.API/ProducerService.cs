using Confluent.Kafka;
using System.Text.Json;

namespace ProjetoKafka.Producer.API;

// Serviço para enviar mensagens para um tópico Kafka
public class ProducerService
{
    // Configuração do serviço Kafka
    private readonly IConfiguration _configuration;

    // Configuração do produtor Kafka
    private readonly ProducerConfig _producerConfig;

    // Logger para registrar informações e erros
    private readonly ILogger<ProducerService> _logger;

    // Construtor do serviço que recebe configurações e logger
    public ProducerService(IConfiguration configuration, ILogger<ProducerService> logger)
    {
        _configuration = configuration;
        _logger = logger;

        // Obtém o endereço do servidor Kafka a partir da configuração
        var bootstrap = _configuration.GetSection("KafkaConfig:BootstrapServer").Value;

        // Configura o produtor Kafka com o endereço do servidor
        _producerConfig = new ProducerConfig()
        {
            BootstrapServers = bootstrap
        };
    }

    // Método assíncrono para enviar uma mensagem para o Kafka
    public async Task<string> SendMessage(Relatorio relatorio)
    {
        try
        {
            // Obtém o nome do tópico a partir da configuração
            var topic = _configuration.GetSection("KafkaConfig:TopicName").Value;

            // Cria um produtor Kafka
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                // Serializa o objeto relatorio para JSON
                var message = JsonSerializer.Serialize(relatorio);

                // Envia a mensagem para o tópico Kafka e aguarda o resultado
                var result = await producer.ProduceAsync(topic: topic, new() { Value = message });

                // Registra a informação do status da mensagem e o conteúdo enviado
                _logger.LogInformation($"{result.Status.ToString()} - {message}");

                // Retorna o status e a mensagem enviada como string
                return $"{result.Status.ToString()} - {message}";
            }
        }
        catch
        {
            // Registra um erro se algo der errado ao enviar a mensagem
            var mensagemDeErro = "Erro ao enviar mensagem";
            _logger.LogError(mensagemDeErro);

            // Retorna a mensagem de erro
            return mensagemDeErro;
        }
    }
}
