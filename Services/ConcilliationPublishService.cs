using System.Text;
using System.Text.Json;
using me_faz_um_pix.Config;
using me_faz_um_pix.Dtos;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public class ConcilliationPublishService(IOptions<QueueConfig> queueConfig)
{
    private readonly string _hostname = queueConfig.Value.Hostname;
    private readonly string _userName = queueConfig.Value.Username;
    private readonly string _password = queueConfig.Value.Password;

    public void SendMessage(ConcilliationDTO dto, long paymenProviderId)
    {

        ConnectionFactory factory = new()
        {
            HostName = _hostname,
            UserName = _userName,
            Password = _password,
        };


        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "concilliation",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        ConcilliationMessageDTO content = new(dto,paymenProviderId);

        string json = JsonSerializer.Serialize(content);
        var body = Encoding.UTF8.GetBytes(json);

        IBasicProperties basicProperties = channel.CreateBasicProperties();
        basicProperties.Persistent = true;

        channel.BasicPublish(
            exchange: String.Empty,
            routingKey: "concilliation",
            basicProperties: basicProperties,
            body: body
        );
    }
}