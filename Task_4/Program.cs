class Program
{
    static void Main(string[] args)
    {
        // Створення вузлів
        var system = new EventSystem();
        var node1 = new Node(1);
        var node2 = new Node(2);
        var node3 = new Node(3);

        // Реєстрація вузлів в системі
        system.RegisterNode(node1);
        system.RegisterNode(node2);
        system.RegisterNode(node3);

        // Генерація подій
        node1.GenerateEvent(system);
        node2.GenerateEvent(system);
        node3.GenerateEvent(system);

        // Виведення всіх подій до сортування
        Console.WriteLine("\nBefore sorting:");
        system.ShowEvents();

        // Сортування подій за часом Лампорта
        system.SortEvents();

        // Виведення всіх подій після сортування
        Console.WriteLine("\nAfter sorting:");
        system.ShowEvents();
    }
}
