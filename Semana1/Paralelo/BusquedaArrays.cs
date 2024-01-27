using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 1. Divide el array en segmentos basados en el número de hilos que deseas usar.
// 2. Ejecuta una búsqueda en cada segmento de manera paralela.
// 3. Si encuentra el número, guarda el índice donde lo encontró.
// 4. Imprime el resultado en la consola.


namespace Semana1.Paralelo
{
    class BusquedaArrays
    {
       
    
        public static void Run()
        {
            int[] array = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31 };
            int target = 17; // El número que queremos buscar
            int numThreads = 4; // Número de hilos para usar en la búsqueda

            int segmentSize = array.Length / numThreads;
            int foundIndex = -1;
            object lockObj = new object();

            Parallel.For(0, numThreads, (i) =>
            {
                int start = i * segmentSize;
                int end = (i == numThreads - 1) ? array.Length : start + segmentSize;

                for (int j = start; j < end && foundIndex == -1; j++)
                {
                    if (array[j] == target)
                    {
                        lock (lockObj)
                        {
                            if (foundIndex == -1)
                            {
                                foundIndex = j;
                            }
                        }
                    }
                }
            });

            if (foundIndex != -1)
            {
                Console.WriteLine($"Número {target} encontrado en el índice: {foundIndex}");
            }
            else
            {
                Console.WriteLine("Número no encontrado.");
            }
        }
    }


}
