using OperationJournal;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var operationLog = new OperationLog();
        var resource = new Resource("Основний ресурс");

        var conflictManager = new ConflictManager(operationLog, resource);

        // Запуск кількох потоків
        var tasks = new Task[5];
        for (int i = 0; i < 5; i++)
        {
            var threadName = $"Thread-{i + 1}";
            tasks[i] = Task.Run(() => conflictManager.ExecuteOperation(threadName));
        }

        Task.WhenAll(tasks).Wait();

        // Виведення результатів
        Console.WriteLine("\nЖурнал операцій:");
        operationLog.PrintLog();
    }
}

