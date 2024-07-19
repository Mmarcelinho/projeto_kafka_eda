using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoKafka.Consumer.Console;

// Cria e configura o host para o serviço de consumidor Kafka
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Adiciona o serviço de consumidor Kafka como um serviço hospedado
        services.AddHostedService<ConsumerService>();
    })
    .Build();

// Executa o host, iniciando o serviço de consumidor Kafka
await host.RunAsync();
