# Arquitetura Orientada a Eventos (Event-Driven Architecture - EDA)

## O que é uma Arquitetura Orientada a Eventos?

A Arquitetura Orientada a Eventos (EDA) é um estilo de arquitetura que se concentra na produção, detecção, consumo e reação a eventos. É particularmente útil em sistemas que requerem alta escalabilidade e resiliência. A EDA é um padrão de design de software que permite a uma organização detectar "eventos" ou momentos de negócios importantes e agir sobre eles em tempo real ou quase real. Ela se baseia na ideia de que os sistemas são compostos por componentes independentes que se comunicam por meio de eventos.

## Conceitos Básicos

### Serviços Produtores e Consumidores de Eventos

- **Serviços Produtores:** São componentes que geram eventos com base em alguma ação ou mudança de estado. Exemplo: um serviço de e-commerce pode gerar um evento quando um pedido é criado.
- **Serviços Consumidores:** São componentes que recebem e processam eventos. Eles realizam ações em resposta aos eventos recebidos, como atualizar uma base de dados ou enviar uma notificação.

### Processamento Baseado em Filas e Tópicos

- Diferente da arquitetura baseada em requisições (request-response), a EDA utiliza mecanismos como filas e tópicos para processar eventos de forma assíncrona.
- Exemplos de tecnologias utilizadas incluem Apache Kafka e RabbitMQ.

### Operação com Requisições

- Embora a EDA seja primariamente orientada a eventos, ela também pode operar em conjunto com requisições tradicionais. Um serviço pode, por exemplo, enviar uma requisição HTTP e, em paralelo, gerar um evento para outros sistemas interessados.

### Eventos e Consumidores

- **Eventos:** São mensagens que representam uma mudança de estado ou uma ação. Exemplo: "Pedido Criado" ou "Usuário Registrado".
- **Consumidores:** São serviços ou sistemas que se inscrevem para receber certos tipos de eventos e executam ações específicas com base neles.

### Suporte a Grande Carga e Escalabilidade

- A EDA é projetada para suportar uma grande quantidade de eventos de múltiplos usuários, permitindo que o sistema escale conforme necessário.
- A escalabilidade é facilitada pela possibilidade de adicionar novos serviços produtores ou consumidores de forma pontual.

### Complexidade e Implementação

- A EDA é uma arquitetura complexa e não deve ser a primeira escolha para todos os projetos. É recomendada para sistemas que realmente necessitam de alta escalabilidade e resiliência.
- Implementar uma EDA requer um bom planejamento e entendimento dos requisitos do sistema.

## Integrantes e Participantes

### Publisher (Publicador)

- Gera e envia eventos para o sistema.
- Exemplo: Um serviço de e-commerce que publica um evento quando um novo pedido é feito.

### Subscriber (Assinante)

- Recebe e processa eventos publicados no sistema.
- Exemplo: Um serviço de envio de e-mails que processa o evento "Novo Pedido" para enviar uma confirmação ao cliente.

### Serviço Broker

- Atua como intermediário para gerenciar a entrega de eventos entre publicadores e assinantes.
- Exemplo: Apache Kafka ou RabbitMQ.

### Event Sourcing

- Um padrão onde o estado do sistema é derivado de uma sequência de eventos.
- Exemplo: Em um sistema financeiro, todas as transações são registradas como eventos, e o saldo da conta é calculado somando todas as transações.

### Gateway

- Ponto de entrada para eventos que vêm de fora do sistema.
- Exemplo: Um API Gateway que recebe requisições HTTP e as converte em eventos para o sistema interno.

## Cenário

![Cenário](cenario1.png)

## BROKER (Apache Kafka)

### Gerenciador de Mensagens (Servidor de Mensageria)

- **Apache Kafka:** Um sistema de mensagens distribuído que usa o modelo publish-subscribe.
- **Alta Disponibilidade:** Kafka é conhecido por sua alta disponibilidade e redundância, o que garante que os eventos sejam entregues mesmo em caso de falhas no sistema.

## Comunicação: Síncrona vs Assíncrona

### Request-Driven (APIs REST)
- **Sincrona**
- Exemplo de Fluxo: 
  ![Request-Driven](request-driven.png)

### Event-Driven
- **Assíncrona**
- Exemplo de Fluxo:
  ![Event-Driven](event-driven.png)

## Estrutura da EDA

Uma arquitetura orientada a eventos consiste em produtores de eventos que geram um fluxo de eventos e consumidores de eventos que escutam os eventos.

![Estrutura da EDA](estrutura-eda.png)

- **Produtores e Consumidores Desacoplados:** Os produtores estão dissociados dos consumidores.
- **Consumidores Desacoplados entre Si:** Os consumidores também estão dissociados uns dos outros.
- **Visibilidade dos Eventos:** Cada consumidor vê todos os eventos.

## Exemplo de Implementação

![Exemplo de Implementação](exemplo1.png)

## EDA - Publisher/Subscriber (pub/sub)

![Pub/Sub](pub-sub.png)

A infraestrutura de mensagens monitora as assinaturas; quando um evento é publicado, ele o envia para cada assinante. Depois que um evento for recebido, ele não pode ser reproduzido e novos assinantes não veem o evento.

## EDA - Event Streaming

![Event Streaming](event-streaming.png)

Os eventos são gravados em um log, são estritamente ordenados (dentro de uma partição) e persistidos. Os clientes não se inscrevem no fluxo; em vez disso, um cliente pode ler de qualquer parte do fluxo. O cliente é responsável por avançar sua posição no fluxo, o que significa que um cliente pode participar a qualquer momento e pode repetir eventos.

## EDA - Padrões de Processamento de Eventos

1. **Eventos Simples:** Os consumidores processam cada evento conforme ele é recebido.
2. **Eventos Complexos:** Os consumidores processam uma série de eventos para detectar padrões nos dados do evento.
3. **Fluxo de Eventos:** Plataformas de streaming de dados ingerem eventos e criam um pipeline para transmitir processadores que transformam e consomem os dados.

## EDA - Benefícios

- Reatividade a eventos em tempo real
- Desacoplamento
- Escalabilidade
- Flexibilidade
- Resiliência
- Integração simplificada
- Auditoria e rastreamento
- Notificação em tempo real
- Processamento paralelo
- Reutilização de componentes

## EDA - Desafios

- Complexidade adicional
- Depuração e rastreamento complexos
- Consistência e integridade de dados
- Ordenação de eventos
- Segurança
- Gerenciamento de estado
- Concorrência e conflitos
- Overhead de comunicação
- Modelagem de eventos adequada
- Tolerância a falhas
- Latência

## EDA - Quando Usar?

- Vários subsistemas devem processar os mesmos eventos
- Processamento em tempo real com atraso mínimo
- Processamento de eventos complexos
- Alto volume e alta velocidade de dados (como IoT)
- Sistemas altamente distribuídos
- Microsserviços
- Processamento de transações financeiras
- Aplicações móveis e web em tempo real
- Eventos complexos de negócios

##  Referências

- [Dotnet e Apache Kafka | Trabalhando com o Broker de mensageria mais utilizado](https://www.youtube.com/watch?v=TJaeu-CQEuU&list=PLDlCoOcmGJFtHvHrVRN5f9p0szeF15R80)
- [# .NET - Apresentando Event-Driven Architecture](https://www.youtube.com/watch?v=9_tpsmdKPkM&list=PLDlCoOcmGJFtHvHrVRN5f9p0szeF15R80&index=2)

## Autores

Estes projetos de exemplo foram criados para fins educacionais. [Marcelo](https://github.com/Mmarcelinho) é responsável pela criação e manutenção destes projetos.

## Licença

Este projetos não possuem uma licença específica e são fornecidos apenas para fins de aprendizado e demonstração.