using System;
using System.IO;
using System.Net.Sockets;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace XC.Library.Common.Tools.RabbitMQ
{
   public  class RabbitMQConnect
    {
        static string host = "127.0.0.1";
        static string UserName = "H";
        static string password = "H";

        public readonly static IConnectionFactory _connectionFactory;
        IConnection _connection;
        object sync_root = new object();
        bool _disposed;
        static RabbitMQConnect()
        {
            //if (host == "localhost")
            //{
            //    _connectionFactory = new ConnectionFactory() { HostName = host };
            //}
            //else
            {
                _connectionFactory = new ConnectionFactory() { HostName = host, UserName = UserName, Password = password };
            }
        }
        public bool IsConnected => this._connection != null && this._connection.IsOpen && this._disposed;
        public IModel CreateModel()
        {
            if (!this.IsConnected)
            {
                this.TryConnect();
            }
            return this._connection.CreateModel();
        }
        public bool TryConnect()
        {
            lock (this.sync_root)
            {
                RetryPolicy policy = RetryPolicy.Handle<SocketException>()//如果我们想指定处理多个异常类型通过OR即可
                    .Or<BrokerUnreachableException>()//ConnectionFactory.CreateConnection期间无法打开连接时抛出异常
                    .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {

                    });// 重试次数，提供等待特定重试尝试的持续时间的函数，每次重试时调用的操作。
                policy.Execute(() =>
                {
                    this._connection = _connectionFactory.CreateConnection();

                });

                if (this.IsConnected)
                {
                    //当连接被破坏时引发。如果在添加事件处理程序时连接已经被销毁对于此事件，事件处理程序将立即被触发。
                    this._connection.ConnectionShutdown += this.OnConnectionShutdown;
                    //在连接调用的回调中发生异常时发出信号。当ConnectionShutdown处理程序抛出异常时，此事件将发出信号。如果将来有更多的事件出现在RabbitMQ.Client.IConnection上，那么这个事件当这些事件处理程序中的一个抛出异常时，它们将被标记。
                    this._connection.CallbackException += this.OnCallbackException;
                    this._connection.ConnectionBlocked += this.OnConnectionBlocked;

                    //LogHelperNLog.Info($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                    return true;
                }
                else
                {
                    // LogHelperNLog.Info("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (this._disposed) return;
            //RabbitMQ连接正在关闭。 尝试重新连接...
            //LogHelperNLog.Info("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            this.TryConnect();
        }
        /// <summary>
        ///   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (this._disposed) return;

            // LogHelperNLog.Info("A RabbitMQ connection throw exception. Trying to re-connect...");

            this.TryConnect();
        }
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (this._disposed) return;

            //  LogHelperNLog.Info("A RabbitMQ connection is shutdown. Trying to re-connect...");

            this.TryConnect();
        }

        public void Dispose()
        {
            if (this._disposed) return;

            this._disposed = true;

            try
            {
                this._connection.Dispose();
            }
            catch (IOException ex)
            {
                //_logger.LogCritical(ex.ToString());
                //  LogHelperNLog.Error(ex);
            }
        }
    }
}
