using System;

public class PagoOnlineService
{
    private static PagoOnlineService instance;
    private static readonly object lockObject = new object();

    // Propiedad pública para acceder a la instancia única
    public static PagoOnlineService Instance
    {
        get
        {
            // Verificar si la instancia ya existe
            if (instance == null)
            {
                // Utilizar el objeto de bloqueo para garantizar la sincronización en hilos concurrentes
                lock (lockObject)
                {
                    // Verificar nuevamente dentro del bloque para evitar la creación de múltiples instancias en hilos concurrentes
                    if (instance == null)
                    {
                        instance = new PagoOnlineService();
                    }
                }
            }

            return instance;
        }
    }


    // Constructor privado para evitar la creación de instancias fuera de la clase
    private PagoOnlineService()
    {
        // Inicialización del servicio
        Console.WriteLine("Se ha creado una instancia del servicio de Pago Online.");
    }

    private void RealizarPago()  { }

    // Método simulado del servicio
    public void RealizarPago(decimal monto)
    {
        Console.WriteLine($"Procesando un pago en línea por {monto:C}.");
        // Lógica de procesamiento del pago
    }
}

class Program
{
    static void Main()
    {
        // Acceder a la instancia única del servicio de Pago Online
        PagoOnlineService servicioPago = PagoOnlineService.Instance;
                
        // Realizar un pago utilizando el servicio
        servicioPago.RealizarPago(50.75m);

        // Intentar crear una nueva instancia (esto devuelve la misma instancia creada anteriormente)
        PagoOnlineService otroServicioPago = PagoOnlineService.Instance;

        // Verificar si son la misma instancia
        Console.WriteLine(object.ReferenceEquals(servicioPago, otroServicioPago)); // Debería imprimir: True
    }
}
