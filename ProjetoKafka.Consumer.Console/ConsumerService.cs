using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProjetoKafka.Consumer.Console;

// Serviço de consumidor Kafka que executa em segundo plano
public class ConsumerService : BackgroundService
{
    // Consumidor Kafka
    private readonly IConsumer<Ignore, string> _consumer;
    // Configurações do consumidor Kafka
    private readonly ConsumerConfig _consumerConfig;
    // Logger para registrar informações e erros
    private readonly ILogger<ConsumerService> _logger;
    // Modelo de parâmetros contendo configurações do Kafka
    private readonly ParametersModel _parameters;

    // Construtor do serviço que recebe um logger
    public ConsumerService(ILogger<ConsumerService> logger)
    {
        _logger = logger;

        // Inicializa o modelo de parâmetros
        _parameters = new ParametersModel();

        // Configura o consumidor Kafka com os parâmetros obtidos
        _consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = _parameters.BootstrapServer,
            GroupId = _parameters.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        // Cria um consumidor Kafka com a configuração fornecida
        _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
    }

    // Método protegido que executa a tarefa assíncrona principal do serviço
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Aguardando mensagens");

        // Inscreve o consumidor no tópico Kafka especificado
        _consumer.Subscribe(_parameters.TopicName);

        // Loop contínuo até que a solicitação de cancelamento seja feita
        while (!stoppingToken.IsCancellationRequested)
        {
            // Executa a tarefa de consumo em uma thread separada
            await Task.Run(() =>
            {
                // Consome uma mensagem do Kafka
                var result = _consumer.Consume(stoppingToken);

                // Desserializa a mensagem JSON para um objeto Relatorio
                var relatorio = JsonSerializer.Deserialize<Relatorio>(result.Message.Value);

                // Atualiza o status e a data de processamento do relatório
                relatorio.Status = StatusRelatorio.CONCLUIDO;
                relatorio.DataProcessamento = DateTime.UtcNow;

                // Registra informações sobre a mensagem recebida
                _logger.LogInformation($"GroupId: {_parameters.GroupId} Mensagem: {result.Message.Value}");
                _logger.LogInformation(relatorio.ToString());
            });
        }
    }

    // Método chamado quando o serviço é interrompido
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        // Fecha a conexão do consumidor Kafka
        _consumer.Close();
        _logger.LogInformation("Aplicação finalizada, conexão fechada");
        return Task.CompletedTask;
    }
}
