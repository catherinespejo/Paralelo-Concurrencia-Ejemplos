using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana1.Paralelo
{

    // 1. Define dos matrices, matrixA y matrixB, y una función MultiplyMatricesParallel para multiplicarlas.
    // 2. La función MultiplyMatricesParallel utiliza Parallel.For para iterar a través de las filas de la primera matriz (matrixA).
    // 3. Cada iteración del bucle Parallel.For calcula una fila completa de la matriz resultante,
    // multiplicando la fila correspondiente de matrixA por cada columna de matrixB.
    // 4. Después de completar todas las tareas paralelas, la función devuelve la matriz resultante.
    class MultiplicacionMatrices
    {
     
    
        public static void Run()
        {
            // Definir dos matrices de ejemplo
            int[,] matrixA = { { 1, 2 }, { 3, 4 } };
            int[,] matrixB = { { 2, 0 }, { 1, 2 } };

            // Imprimir las matrices originales
            Console.WriteLine("Matriz A:");
            PrintMatrix(matrixA);
            Console.WriteLine("\nMatriz B:");
            PrintMatrix(matrixB);

            // Realizar la multiplicación
            int[,] resultMatrix = MultiplyMatricesParallel(matrixA, matrixB);

            // Imprimir la matriz resultante
            Console.WriteLine("\nResultado de la Multiplicación:");
            PrintMatrix(resultMatrix);
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static int[,] MultiplyMatricesParallel(int[,] matrixA, int[,] matrixB)
        {
            int aRows = matrixA.GetLength(0);
            int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0);
            int bCols = matrixB.GetLength(1);
            int[,] result = new int[aRows, bCols];

            if (aCols != bRows)
                throw new InvalidOperationException("Las matrices no se pueden multiplicar.");

            Parallel.For(0, aRows, i =>
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            });

            return result;
        }
    }

}




