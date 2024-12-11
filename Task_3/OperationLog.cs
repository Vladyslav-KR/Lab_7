using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OperationJournal
{
    // Клас для журналу операцій
    public class OperationLog
    {
        public ConcurrentQueue<string> Log { get; private set; }

        public OperationLog()
        {
            Log = new ConcurrentQueue<string>();
        }

        // Запис в журнал
        public void AddOperation(string operation)
        {
            Log.Enqueue(operation);
        }

        // Виведення всіх операцій в журналі
        public void PrintLog()
        {
            foreach (var operation in Log)
            {
                Console.WriteLine(operation);
            }
        }
    }
}
