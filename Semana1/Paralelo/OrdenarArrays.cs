using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana1.Paralelo {


   // 1. Define un array de enteros para ordenar.
   // 2. Llama a la función ParallelMergeSort, que es una implementación paralela del algoritmo de Merge Sort.
   // 3. La función ParallelMergeSort divide el array en dos mitades y
   // llama a sí misma recursivamente para cada mitad, utilizando Parallel.Invoke para hacer estas llamadas en paralelo.
   // 4. Una vez que las mitades están ordenadas, se combinan utilizando la función Merge.

    class OrdernarArrays 
{

    public static void Run()
    {
        int[] array = { 3, 6, 4, 2, 11, 10, 5, 15, 1, 14, 12, 13, 9, 8, 7, 0 };
            // Imprimir el array original
            Console.WriteLine("Array Original:");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();


            // Realizar el ordenamiento
            ParallelMergeSort(array, 0, array.Length - 1);

        // Imprimir el array ordenado
        Console.WriteLine("Array Ordenado:");
        foreach (var item in array)
        {
            Console.Write(item + " ");
        }
    }

    public static void ParallelMergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            // Ordenar en paralelo las dos mitades
            Parallel.Invoke(
                () => ParallelMergeSort(array, left, middle),
                () => ParallelMergeSort(array, middle + 1, right)
            );

            // Combinar las mitades ordenadas
            Merge(array, left, middle, right);
        }
    }

    public static void Merge(int[] array, int left, int middle, int right)
    {
        int[] leftArray = new int[middle - left + 1];
        int[] rightArray = new int[right - middle];

        Array.Copy(array, left, leftArray, 0, middle - left + 1);
        Array.Copy(array, middle + 1, rightArray, 0, right - middle);

        int i = 0, j = 0, k = left;

        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < leftArray.Length)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < rightArray.Length)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }
}
}




