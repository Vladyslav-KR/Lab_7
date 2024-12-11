using System;
using System.Threading.Tasks;
using DistributedSystem;

namespace DistributedSystemExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення вузлів
            var nodeA = new DistributedSystemNode("Вузол A");
            var nodeB = new DistributedSystemNode("Вузол B");
            var nodeC = new DistributedSystemNode("Вузол C");

            // Підписка на події зміни статусу
            nodeA.StatusChanged += (sender, status) => Console.WriteLine(status);
            nodeB.StatusChanged += (sender, status) => Console.WriteLine(status);
            nodeC.StatusChanged += (sender, status) => Console.WriteLine(status);

            // З'єднання вузлів
            nodeA.ConnectToNode(nodeB);
            nodeB.ConnectToNode(nodeC);

            // Відправка повідомлень
            nodeA.SendMessage("Привіт, B!", nodeB);
            nodeB.SendMessage("Привіт, C!", nodeC);
            nodeC.SendMessage("Привіт, A!", nodeA);

            // Зміна статусу вузлів
            await Task.Delay(2000);
            nodeB.ChangeStatus(false);

            await Task.Delay(2000);
            nodeB.ChangeStatus(true);

            // Завершення роботи
            await Task.Delay(2000);
            nodeA.ShutDown();
            nodeB.ShutDown();
            nodeC.ShutDown();
        }
    }
}
