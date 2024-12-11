using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedSystem
{
    public class DistributedSystemNode
    {
        public string NodeName { get; }
        public bool IsActive { get; private set; }

        // Подія для повідомлення про зміну статусу вузла
        public event EventHandler<string> StatusChanged;

        private readonly ConcurrentQueue<string> _messageQueue;
        private readonly List<DistributedSystemNode> _connectedNodes;
        private readonly CancellationTokenSource _cts;

        public DistributedSystemNode(string name)
        {
            NodeName = name;
            IsActive = true;
            _messageQueue = new ConcurrentQueue<string>();
            _connectedNodes = new List<DistributedSystemNode>();
            _cts = new CancellationTokenSource();

            // Запуск обробки повідомлень
            Task.Run(() => ProcessMessagesAsync(_cts.Token));
        }

        // Метод для з'єднання з іншим вузлом
        public void ConnectToNode(DistributedSystemNode otherNode)
        {
            _connectedNodes.Add(otherNode);
        }

        // Відправка повідомлення іншому вузлу
        public void SendMessage(string message, DistributedSystemNode targetNode)
        {
            if (IsActive && targetNode.IsActive)
            {
                Console.WriteLine($"{NodeName} надсилає повідомлення '{message}' до {targetNode.NodeName}");
                targetNode.ReceiveMessage($"{NodeName}: {message}");
            }
            else
            {
                Console.WriteLine($"{NodeName} або {targetNode.NodeName} неактивні. Повідомлення не відправлено.");
            }
        }

        // Прийом повідомлення
        public void ReceiveMessage(string message)
        {
            _messageQueue.Enqueue(message);
        }

        // Асинхронна обробка повідомлень
        private async Task ProcessMessagesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_messageQueue.TryDequeue(out string message))
                {
                    Console.WriteLine($"{NodeName} отримав повідомлення: {message}");
                    await Task.Delay(500); // Імітація затримки обробки повідомлення
                }
                else
                {
                    await Task.Delay(100); // Чекати, якщо черга порожня
                }
            }
        }

        // Зміна статусу вузла
        public void ChangeStatus(bool isActive)
        {
            IsActive = isActive;
            OnStatusChanged($"{NodeName} тепер {(IsActive ? "активний" : "неактивний")}.");
        }

        // Виклик події StatusChanged
        protected virtual void OnStatusChanged(string statusMessage)
        {
            StatusChanged?.Invoke(this, statusMessage);
        }

        // Завершення роботи вузла
        public void ShutDown()
        {
            ChangeStatus(false);
            _cts.Cancel();
        }
    }
}
