// Клас для симуляції ресурсу, з яким працюють потоки
using OperationJournal;

public class Resource
{
    public string ResourceName { get; set; }
    private readonly object _lock = new object();

    public Resource(string name)
    {
        ResourceName = name;
    }

    // Метод для зміни ресурсу
    public void ChangeResource(string threadName)
    {
        lock (_lock)
        {
            Console.WriteLine($"[Thread: {threadName}] змінює ресурс: {ResourceName}");
            Thread.Sleep(500); // Імітація часу на зміну ресурсу
        }
    }
}

