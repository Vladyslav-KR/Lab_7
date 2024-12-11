public class EventSystem
{
    private List<Node> _nodes = new List<Node>();
    private List<Event> _events = new List<Event>();

    public void RegisterNode(Node node)
    {
        _nodes.Add(node);
        node.OnEventOccurred += HandleEvent; // Підписка на події
    }

    public void UnregisterNode(Node node)
    {
        _nodes.Remove(node);
        node.OnEventOccurred -= HandleEvent;
    }

    // Обробка подій від різних вузлів
    private void HandleEvent(Event e)
    {
        Console.WriteLine($"Event {e} has occurred.");
        foreach (var node in _nodes)
        {
            if (node.NodeId != e.NodeId)
                node.ReceiveEvent(e); // Сповіщення інших вузлів про подію
        }
        _events.Add(e); // Додавання події до системи
    }

    // Реєстрація нової події
    public void RegisterEvent(Event newEvent)
    {
        _events.Add(newEvent);
    }

    // Сортування подій по часі за допомогою алгоритму Лампорта
    public void SortEvents()
    {
        _events = _events.OrderBy(e => e.Timestamp).ThenBy(e => e.NodeId).ToList();
    }

    public void ShowEvents()
    {
        foreach (var e in _events)
        {
            Console.WriteLine(e);
        }
    }
}

