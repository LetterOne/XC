using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Client;
using System.Text;

namespace XC.Library.Common.Tools.RabbitMQ
{

        public class RabbitMQSend
        {
            /// <summary>
            /// Newtonsoft.Json利用IsoDateTimeConverter处理日期类型
            /// </summary>
            static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            static RabbitMQConnect connection = null;

            static RabbitMQSend()
            {
                connection = new RabbitMQConnect();
            }

            /// <summary>
            /// 添加信息到队列
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="item">信息</param>
            /// <param name="queueName">队列名</param>
            public static void PushMsgToMq<T>(T item, string queueName)
            {
                string msg = JsonConvert.SerializeObject(item, dtConverter);
                using (global::RabbitMQ.Client.IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    //Construct a completely empty content header for use with the Basic content class.
                    //构造一个完全空的内容标头，以便与Basic内容类一起使用。
                    global::RabbitMQ.Client.IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    byte[] body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: "",
                        routingKey: queueName,
                        basicProperties: properties,
                        body: body);
                }
            }
        }
    
}
