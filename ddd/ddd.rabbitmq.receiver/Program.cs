using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.DispatchConsumersAsync = true;
var exchangeName = "exchange1";
var eventName = "myEvent";
using var conn = factory.CreateConnection();
using var channel = conn.CreateModel();
var queueName = "queue1";
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: eventName);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
Console.ReadLine();

async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
{
    try
    {
        var bytes = args.Body.ToArray();
        var msg = Encoding.UTF8.GetString(bytes);
        System.Console.WriteLine(DateTime.Now + " received the msg: " + msg);
        channel.BasicAck(args.DeliveryTag, multiple: false);
        await Task.Delay(800);
    }
    catch (Exception ex)
    {
        channel.BasicReject(args.DeliveryTag, true); //fail to resend
        System.Console.WriteLine("Fail to handle the msg: " + ex);
    }
}