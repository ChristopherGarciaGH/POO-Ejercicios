/*
 Ejemplo con POO: herencia, poliformismo, encapsulación
 Añade características C#: generics y linq.
 */


using System;
using System.Collections.Generic; // Generics!!!
using System.Linq; // Linq!!!

public interface ISaludable
{
    double CalcularIMC();
    bool IsSaludable();
}

public class Persona
{
    private int _id = 0;
    private string _nombre = string.Empty;
    private int _peso = 0;
    private int _altura = 0;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }

    public int Peso
    {
        get { return _peso; }
        set
        {
            if (value > 0)
                _peso = value;
            else
                throw new ArgumentException("El peso debe ser mayor a 0.");
        }
    }

    public int Altura
    {
        get { return _altura; }
        set
        {
            if (value > 0)
                _altura = value;
            else
                throw new ArgumentException("La altura debe ser mayor a 0.");
        }
    }

    public Persona(int id, string nombre, int peso, int altura)
    {
        Id = id;
        Nombre = nombre;

        // Peso y altura ya llevan la validación de valores > 0 porque estamos asignando la propiedad Peso y Altura y no los fields _peso y _altura!!!
        Peso = peso;     
        Altura = altura; 
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nombre: {Nombre}, Altura: {Altura} cm, Peso: {Peso} kg";
    }
}

public class Alumno : Persona, ISaludable
{
    public Alumno(int id, string nombre, int peso, int altura) : base(id, nombre, peso, altura) { }

    public double CalcularIMC()
    {
        // Implementa el cálculo del IMC para alumnos
        double alturaEnMetros = Altura / 100.0;
        return Peso / (alturaEnMetros * alturaEnMetros);
    }

    public bool IsSaludable() => (this.CalcularIMC() < 10);
}



public class Profesor : Persona, ISaludable
{
    public Profesor(int id, string nombre, int peso, int altura) : base(id, nombre, peso, altura) { }

    public double CalcularIMC()
    {
        // Implementa el cálculo del IMC para profesores (adultos -5)
        double alturaEnMetros = Altura / 100.0;
        return Peso / (alturaEnMetros * alturaEnMetros) - 5;
    }

    public bool IsSaludable() => (this.CalcularIMC() < 20);
}

// Generic class!!! CalculadoraIMC<T>
public class CalculadoraIMC<T> where T : Persona, ISaludable
{
    private readonly List<T> _lista;

    public CalculadoraIMC(List<T> lista)
    {
        _lista = lista ?? throw new ArgumentNullException(nameof(lista));

        // Detectar duplicados por Id y eliminarlos
        EliminarDuplicados();
    }

    public void AddItem(T item)
    {
        // Antes de añadir a la lista, comprobamos si existe
        if (_lista.Find(i => i.Id == item.Id) == null)
        {
            _lista.Add(item);
        }
    }

    public void RemoveItem(T item) => _lista.Remove(item);

    public void Clear() => _lista.Clear();

    public double CalcularPromedioIMC()
    {
        if (_lista.Count == 0)
        {
            throw new InvalidOperationException("La lista no puede estar vacía.");
        }

        // Linq (programación funcional) y Poliformismo!!!! Oh là là! 
        return _lista.Average(x => x.CalcularIMC());
    }

    private void EliminarDuplicados()
    {
        var idsUnicos = new HashSet<int>(); // Generics con collections built-in de .net!!! HashSet<int>
        _lista.RemoveAll(item => !idsUnicos.Add(item.Id));
    }
}

class Program
{
    static void Main()
    {
        // Crear una lista de alumnos
        List<Alumno> alumnos = new List<Alumno>
        {
            new Alumno(1, "Manuel", 80, 180),
            new Alumno(2, "Pablo", 75, 170),
            // Agregar más alumnos según sea necesario
        };

        // Crear una calculadora de IMC para alumnos
        var calculadoraAlumnos = new CalculadoraIMC<Alumno>(alumnos);

        // Calcular y mostrar el promedio del IMC para alumnos
        double promedioIMCAlumnos = calculadoraAlumnos.CalcularPromedioIMC();
        Console.WriteLine($"Promedio IMC Alumnos: {promedioIMCAlumnos:F2}");

        // Crear una lista de profesores
        List<Profesor> profesores = new List<Profesor>
        {
            new Profesor(1, "Jose", 80, 180),
            new Profesor(2, "Juan", 75, 170),
            // Agregar más profesores según sea necesario
        };


        // Crear una calculadora de IMC para profesores
        var calculadoraProfesores = new CalculadoraIMC<Profesor>(profesores);

        // Calcular y mostrar el promedio del IMC para profesores
        double promedioIMCProfesores = calculadoraProfesores.CalcularPromedioIMC();
        Console.WriteLine($"Promedio IMC Profesores: {promedioIMCProfesores:F2}");
    }
}



