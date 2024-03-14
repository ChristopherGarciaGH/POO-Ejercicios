using System;

// Definición de la clase Nodo
public class Nodo<T>
{
    public T Valor { get; set; }
    public Nodo<T> Siguiente { get; set; }

    public Nodo(T valor)
    {
        Valor = valor;
        Siguiente = null;
    }
}

// Definición de la clase Lista
public class Lista<T>
{
    private Nodo<T> cabeza;

    public Lista()
    {
        cabeza = null;
    }

    // Método para agregar un elemento al final de la lista
    public void Agregar(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo<T> actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = nuevoNodo;
        }
    }

    // Método para imprimir los elementos de la lista
    public void Imprimir()
    {
        Nodo<T> actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Valor + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}



// Definición de la interfaz para manejar solicitudes
public interface IApprover
{
    IApprover NextApprover { get; set; }
    void ProcessRequest(ExpenseRequest request);
}

// Clase base para los aprobadores
public abstract class Approver : IApprover
{
    public IApprover NextApprover { get; set; }

    public abstract void ProcessRequest(ExpenseRequest request);
}

// Aprobador concreto de nivel bajo
public class JuniorManager : Approver
{
    public override void ProcessRequest(ExpenseRequest request)
    {
        if (request.Amount <= 1000)
        {
            Console.WriteLine($"La solicitud de ${request.Amount} ha sido aprobada por el Junior Manager.");
        }
        else if (NextApprover != null)
        {
            NextApprover.ProcessRequest(request);
        }
    }
}

// Aprobador concreto de nivel medio
public class SeniorManager : Approver
{
    public override void ProcessRequest(ExpenseRequest request)
    {
        if (request.Amount > 1000 && request.Amount <= 5000)
        {
            Console.WriteLine($"La solicitud de ${request.Amount} ha sido aprobada por el Senior Manager.");
        }
        else if (NextApprover != null)
        {
            NextApprover.ProcessRequest(request);
        }
    }
}

// Aprobador concreto de nivel alto
public class CEO : Approver
{
    public override void ProcessRequest(ExpenseRequest request)
    {
        if (request.Amount > 5000)
        {
            Console.WriteLine($"La solicitud de ${request.Amount} ha sido aprobada por el CEO.");
        }
        else
        {
            Console.WriteLine("La solicitud no puede ser aprobada.");
        }
    }
}

// Clase que representa una solicitud de reembolso
public class ExpenseRequest
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public ExpenseRequest(string description, decimal amount)
    {
        Description = description;
        Amount = amount;
    }
}

class Program
{
    static void Main()
    {
        // Configuración de la cadena de responsabilidad
        IApprover juniorManager = new JuniorManager();
        IApprover seniorManager = new SeniorManager();
        IApprover ceo = new CEO();
                
        juniorManager.NextApprover = seniorManager;
        seniorManager.NextApprover = ceo;


        // Ejemplo de solicitud de reembolso
        var expenseRequest = new ExpenseRequest("Compra de suministros de oficina", 3000);
        juniorManager.ProcessRequest(expenseRequest);


        // Otra forma de utilizar el patrón con una estructura de datos customizada y más extensible: estado y comportamiento

        // Ejemplo de uso
        Lista<IApprover> miLista = new Lista<IApprover>();
        miLista.Agregar(juniorManager);
        miLista.Agregar(seniorManager);
        miLista.Agregar(ceo);

        Console.WriteLine("Elementos de la lista:");
        miLista.Imprimir();
    }
}
