using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semana1.Paralelo;
using Semana1.Concurrencia;

namespace Semana1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Seleccione un algoritmo para ejecutar:");
            Console.WriteLine("1. Busqueda en paralelo de un Array");
            Console.WriteLine("2. Multiplicacion en paralelo de 2 matrices");
            Console.WriteLine("3. Ordenamiento en paralelo de un Array");
            Console.WriteLine("4. Simulador Bancario: depósito, retiro y consulta de saldo");
            Console.WriteLine("5. Simular suministros, productores y consumidores");


            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    BusquedaArrays.Run(); 
                    break;

                case "2":
                    MultiplicacionMatrices.Run(); 
                    break;

                case "3":
                    OrdernarArrays.Run();
                    break;

                case "4":
                    SimuladorBanco.Run();
                    break;
                case "5":
                    ProductoConsumidor.Run();
                    break;



                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            // Esperar una entrada del usuario antes de cerrar la consola
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

    }
}
