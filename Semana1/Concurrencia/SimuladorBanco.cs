using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//Define una clase CuentaBancaria con operaciones básicas como depósito, retiro y consulta de saldo.
//Define una clase Banco que administra múltiples CuentaBancarias y realiza transacciones de manera segura
//utilizando un objeto de bloqueo (lockObj) para evitar condiciones de carrera.
//En el método Main, se crean y ejecutan hilos que simulan transacciones concurrentes en las cuentas bancarias.

//Task.Run, que inicia una operación en un hilo del pool de hilos.
//Esto es más eficiente en términos de gestión de recursos en comparación con la creación de hilos nuevos.

namespace Semana1.Concurrencia
{
    class SimuladorBanco
    {
        public static async Task Run()
        {
            Banco banco = new Banco();
            banco.AbrirCuenta(new CuentaBancaria(1, 1000)); // Cuenta con ID 1 y saldo inicial de 1000
            banco.AbrirCuenta(new CuentaBancaria(2, 500));  // Cuenta con ID 2 y saldo inicial de 500

            // Imprimir el saldo inicial de las cuentas
            Console.WriteLine("Saldo inicial de las cuentas:");
            banco.ImprimirSaldoCuentas();

            // Crear y ejecutar tareas para simular transacciones
            Task t1 = Task.Run(() => banco.RealizarTransaccion(2, 200, TipoTransaccion.Deposito));
            Task t2 = Task.Run(() => banco.RealizarTransaccion(2, 300, TipoTransaccion.Retiro));

            await Task.WhenAll(t1, t2);

            // Imprimir el saldo final de las cuentas
            Console.WriteLine("\nSaldo final de las cuentas:");
            banco.ImprimirSaldoCuentas();
        }
    }

    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    public class CuentaBancaria
    {
        public int Id { get; private set; }
        private decimal saldo;

        public CuentaBancaria(int id, decimal saldoInicial)
        {
            Id = id;
            saldo = saldoInicial;
        }

        public void Depositar(decimal monto)
        {
            saldo += monto;
        }

        public bool Retirar(decimal monto)
        {
            if (saldo >= monto)
            {
                saldo -= monto;
                return true;
            }
            return false;
        }

        public decimal ConsultarSaldo()
        {
            return saldo;
        }
    }

    class Banco
    {
        private Dictionary<int, CuentaBancaria> cuentas = new Dictionary<int, CuentaBancaria>();
        private object lockObj = new object();

        public void AbrirCuenta(CuentaBancaria cuenta)
        {
            lock (lockObj)
            {
                cuentas[cuenta.Id] = cuenta;
            }
        }

        public void RealizarTransaccion(int idCuenta, decimal monto, TipoTransaccion tipo)
        {
            lock (lockObj)
            {
                CuentaBancaria cuenta;
                if (cuentas.TryGetValue(idCuenta, out cuenta))
                {
                    switch (tipo)
                    {
                        case TipoTransaccion.Deposito:
                            cuenta.Depositar(monto);
                            Console.WriteLine($"Depósito realizado: Cuenta {idCuenta}, Monto: {monto}");
                            break;
                        case TipoTransaccion.Retiro:
                            if (!cuenta.Retirar(monto))
                            {
                                Console.WriteLine($"No se pudo retirar: Saldo insuficiente en la cuenta {idCuenta}");
                            }
                            else
                            {
                                Console.WriteLine($"Retiro realizado: Cuenta {idCuenta}, Monto: {monto}");
                            }
                            break;
                    }
                }
            }
        }

        public void ImprimirSaldoCuentas()
        {
            lock (lockObj)
            {
                foreach (var cuenta in cuentas.Values)
                {
                    Console.WriteLine($"Cuenta {cuenta.Id}, Saldo: {cuenta.ConsultarSaldo()}");
                }
            }
        }
    }
}
