// Клас для потоків, що працюють із ресурсами
using ResourceSynchronization;

public class TaskWorker
{
    public int Priority { get; }
    public string WorkerName { get; }
    private List<Resource> _requiredResources;

    public TaskWorker(string name, int priority, List<Resource> requiredResources)
    {
        WorkerName = name;
        Priority = priority;
        _requiredResources = requiredResources;
    }

    public void Run()
    {
        Console.WriteLine($"{WorkerName} із пріоритетом {Priority} чекає на ресурси...");

        foreach (var resource in _requiredResources)
        {
            while (!resource.Acquire(1000))
            {
                Console.WriteLine($"{WorkerName} чекає доступу до {resource.Name}");
            }
            Console.WriteLine($"{WorkerName} отримав доступ до {resource.Name}");
        }

        // Імітація роботи
        Console.WriteLine($"{WorkerName} використовує ресурси...");
        Thread.Sleep(2000);

        // Звільнення ресурсів
        foreach (var resource in _requiredResources)
        {
            resource.Release();
            Console.WriteLine($"{WorkerName} звільнив ресурс {resource.Name}");
        }
    }
}