using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.DispatchConsumersAsync = true;
var exchangeName = "exchange1";
var eventName = "myEvent";
using var conn = factory.CreateConnection();
while (true)
{
    var msg = DateTime.Now.TimeOfDay.ToString();
    using var channel = conn.CreateModel();
    var properties = channel.CreateBasicProperties();
    properties.DeliveryMode = 2;
    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
    var body = Encoding.UTF8.GetBytes(msg);
    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: eventName,
        mandatory: true,
        basicProperties: properties,
        body: body
    );
    System.Console.WriteLine("Publish the message: " + msg);
    Thread.Sleep(1000);
}