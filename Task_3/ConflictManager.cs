// Клас для управління конфліктами
using OperationJournal;

public class ConflictManager
{
    private readonly OperationLog _operationLog;
    private readonly Resource _resource;

    public ConflictManager(OperationLog operationLog, Resource resource)
    {
        _operationLog = operationLog;
        _resource = resource;
    }

    // Запуск операцій
    public void ExecuteOperation(string threadName)
    {
        var operation = $"Thread {threadName} почав змінювати ресурс: {_resource.ResourceName} о {DateTime.Now}";
        _operationLog.AddOperation(operation);

        try
        {
            _resource.ChangeResource(threadName);
        }
        catch (Exception ex)
        {
            // Логування і вирішення конфлікту
            _operationLog.AddOperation($"Конфлікт! {threadName}: {ex.Message} о {DateTime.Now}");
        }

        operation = $"Thread {threadName} завершив зміну ресурсу: {_resource.ResourceName} о {DateTime.Now}";
        _operationLog.AddOperation(operation);
    }
}
