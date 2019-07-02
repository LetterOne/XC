using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace XC.Library.Common.Tools.RabbitMQ
{
   public  class RabbitMQReceive : IDisposable
    {
        IConnection connection = null;
        IModel channel = null;

        public void BindReceiveMqMsg<T>(Func<T, bool> func, Action<string> log, string queueName)
        {
            this.connection = RabbitMQConnect._connectionFactory.CreateConnection();//创建与指定端点的连接。
            this.channel = this.connection.CreateModel(); //创建并返回新的频道，会话和模型。
            this.channel.QueueDeclare(queue: queueName,//队列名称
                             durable: true,//是否持久化, 队列的声明默认是存放到内存中的，如果rabbitmq重启会丢失，如果想重启之后还存在就要使队列持久化，保存到Erlang自带的Mnesia数据库中，当rabbitmq重启之后会读取该数据库
                             exclusive: false,//是否排外的，有两个作用，一：当连接关闭时connection.close()该队列是否会自动删除；二：该队列是否是私有的private，如果不是排外的，可以使用两个消费者都访问同一个队列，没有任何问题，如果是排外的，会对当前队列加锁，其他通道channel是不能访问的，如果强制访问会报异常：com.rabbitmq.client.ShutdownSignalException: channel error; protocol method: #method<channel.close>(reply-code=405, reply-text=RESOURCE_LOCKED - cannot obtain exclusive access to locked queue 'queue_name' in vhost '/', class-id=50, method-id=20)一般等于true的话用于一个队列只能有一个消费者来消费的场景
                             autoDelete: false,//是否自动删除，当最后一个消费者断开连接之后队列是否自动被删除，可以通过RabbitMQ Management，查看某个队列的消费者数量，当consumers = 0时队列就会自动删除
                             arguments: null);//队列中的消息什么时候会自动被删除？

            this.channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//（Spec方法）配置Basic内容类的QoS参数。
                                                                                    //第一个参数是可接收消息的大小的  0不受限制
                                                                                    //第二个参数是处理消息最大的数量  1 那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息，消息只会在队列中阻塞
                                                                                    //第三个参数则设置了是不是针对整个Connection的，因为一个Connection可以有多个Channel，如果是false则说明只是针对于这个Channel的。
            EventingBasicConsumer consumer = new EventingBasicConsumer(this.channel);//构造函数，它将Model属性设置为给定值。
            consumer.Received += (model, bdea) =>
            {
                byte[] body = bdea.Body;
                string message = Encoding.UTF8.GetString(body);
                log?.Invoke(message);

                T item = JsonConvert.DeserializeObject<T>(message);
                bool result = func(item);
                if (result)
                {
                    //（Spec方法）确认一个或多个已传送的消息。
                    this.channel.BasicAck(deliveryTag: bdea.DeliveryTag, multiple: false);
                }
            };
            this.channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer); //The consumer is started with noAck = false(i.e.BasicAck is required), an empty consumer tag (i.e. the server creates and returns a fresh consumer tag), noLocal=false and exclusive=false.
        }
        public void Dispose()
        {
            if (this.channel != null)
            {
                this.channel.Close();
            }

            if (this.connection != null)
            {
                this.connection.Close();
            }
        }
    }
}
