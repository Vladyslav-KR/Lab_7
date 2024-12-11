

public class Node
{
    public int NodeId { get; private set; }
    private int _clock = 0;

    public event Action<Event> OnEventOccurred; // Подія, яку генерує цей вузол

    public Node(int nodeId)
    {
        NodeId = nodeId;
    }

    // Генерація події
    public void GenerateEvent(EventSystem system)
    {
        _clock++; // збільшення часу події
        var newEvent = new Event(_clock, _clock, NodeId);
        system.RegisterEvent(newEvent); // реєстрація події в системі
        OnEventOccurred?.Invoke(newEvent); // Виникнення події
    }

    // Отримання події від іншого вузла
    public void ReceiveEvent(Event incomingEvent)
    {
        _clock = Math.Max(_clock, incomingEvent.Timestamp) + 1;
        Console.WriteLine($"Node {NodeId} received {incomingEvent}");
    }
}
