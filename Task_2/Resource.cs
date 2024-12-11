using System;
using System.Collections.Generic;
using System.Threading;

namespace ResourceSynchronization
{
    // Клас, що представляє ресурс
    public class Resource
    {
        public string Name { get; }
        private SemaphoreSlim _semaphore;

        public Resource(string name, int capacity)
        {
            Name = name;
            _semaphore = new SemaphoreSlim(capacity, capacity);
        }

        // Метод для отримання доступу до ресурсу
        public bool Acquire(int timeout)
        {
            return _semaphore.Wait(timeout);
        }

        // Метод для звільнення ресурсу
        public void Release()
        {
            _semaphore.Release();
        }
    }

}
