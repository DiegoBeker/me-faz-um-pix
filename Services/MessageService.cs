using System.Text;
using System.Text.Json;
using me_faz_um_pix.Config;
using me_faz_um_pix.Dtos;
using me_faz_um_pix.Models;
using me_faz_um_pix.Views;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public class MessageService(IOptions<QueueConfig> queueConfig)
{
    private readonly string _hostname = queueConfig.Value.Hostname;
    private readonly string _queue = queueConfig.Value.Queue;
    private readonly string _userName = queueConfig.Value.Username;
    private readonly string _password = queueConfig.Value.Password;

    public void SendMessage(CreatePaymentView payment, DateTime creationTime)
    {
        ConnectionFactory factory = new()
        {
            HostName = _hostname,
            UserName = _userName,
            Password = _password
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "payments",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        string json = JsonSerializer.Serialize(payment);
        var body = Encoding.UTF8.GetBytes(json);

        IBasicProperties basicProperties = channel.CreateBasicProperties();
        basicProperties.Persistent = true;

        basicProperties.Headers = new Dictionary<string, object>
        {
            { "creation_time", creationTime.ToString() }
        };
        

        channel.BasicPublish(
            exchange: String.Empty,
            routingKey: _queue,
            basicProperties: basicProperties,
            body: body
        );
    }
}