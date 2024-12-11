// Менеджер ресурсів і завдань
public class ResourceManager
{
    private List<TaskWorker> _workers;
    private Mutex _mutex;

    public ResourceManager()
    {
        _workers = new List<TaskWorker>();
        _mutex = new Mutex();
    }

    public void AddWorker(TaskWorker worker)
    {
        _mutex.WaitOne();
        _workers.Add(worker);
        _workers.Sort((w1, w2) => w2.Priority.CompareTo(w1.Priority)); // Сортування за пріоритетом
        _mutex.ReleaseMutex();
    }

    public void Start()
    {
        foreach (var worker in _workers)
        {
            var thread = new Thread(worker.Run);
            thread.Start();
        }
    }
}
