using System;
using System.Collections.Generic;
using System.Threading;
using ResourceSynchronization;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створення ресурсів
        var cpu = new Resource("CPU", 2); // Доступно 2 "ядра"
        var ram = new Resource("RAM", 1); // Доступно 1 "блок"
        var disk = new Resource("Disk", 1); // Доступно 1 "диск"

        // Створення потоків із різними пріоритетами
        var worker1 = new TaskWorker("Worker 1", 1, new List<Resource> { cpu, ram });
        var worker2 = new TaskWorker("Worker 2", 2, new List<Resource> { ram, disk });
        var worker3 = new TaskWorker("Worker 3", 3, new List<Resource> { cpu, disk });

        // Додавання потоків у менеджер
        var manager = new ResourceManager();
        manager.AddWorker(worker1);
        manager.AddWorker(worker2);
        manager.AddWorker(worker3);

        // Запуск потоків
        manager.Start();

        Console.ReadLine(); // Затримка для перегляду результатів
    }
}





