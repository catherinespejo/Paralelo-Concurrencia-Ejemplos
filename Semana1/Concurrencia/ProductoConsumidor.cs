using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Semana1.Concurrencia
{
    class ProductoConsumidor
    {

        public static void Run()
        {
            BlockingCollection<int> buffer = new BlockingCollection<int>(5); // Buffer con capacidad para 5 elementos
            int count = 0; // Contador para seguir el número de elementos en el buffer

            Task producer = Task.Run(() => Producer(buffer, ref count));
            Task consumer = Task.Run(() => Consumer(buffer, ref count));

            Task.WaitAll(producer, consumer);

            Console.WriteLine("Proceso completado.");
        }

        static void Producer(BlockingCollection<int> buffer, ref int count)
        {
            for (int i = 0; i < 10; i++)
            {
                buffer.Add(i);
                Interlocked.Increment(ref count); // Aumentar de forma segura el contador
                Console.WriteLine($"Producido: {i}, Elementos en el buffer: {count}");
                Thread.Sleep(1000); // Simular trabajo
            }
            buffer.CompleteAdding(); // Indicar que ya no se producirán más elementos
        }

        static void Consumer(BlockingCollection<int> buffer, ref int count)
        {
            foreach (var item in buffer.GetConsumingEnumerable())
            {
                Interlocked.Decrement(ref count); // Disminuir de forma segura el contador
                Console.WriteLine($"Consumido: {item}, Elementos en el buffer: {count}");
                Thread.Sleep(1500); // Simular trabajo
            }
        }
    }
}



