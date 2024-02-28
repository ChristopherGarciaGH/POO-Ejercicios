using System;
using System.Runtime.InteropServices.ObjectiveC;

public abstract class Vehiculo
{    
    private string Marca;
    protected string Modelo;
    protected int AñoFabricacion;

    public Vehiculo(string marca, string modelo, int añoFabricacion)
    {
        Marca = marca;
        Modelo = modelo;
        AñoFabricacion = añoFabricacion;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Vehículo: {Marca} {Modelo} ({AñoFabricacion})");
    }

    public abstract void Conducir();
}

public class Automovil : Vehiculo
{    
    public string MarcaRuedas { get; set; }

    public Automovil(string marcaRuedas, string marca, string modelo, int añoFabricacion)
        : base(marca, modelo, añoFabricacion)
    {
        MarcaRuedas = marcaRuedas;

    }

    public override void Conducir()
    {
        Console.WriteLine("Conduciendo el automóvil por la carretera.");
    }
}

public class Motocicleta : Vehiculo
{
    public Motocicleta(string marca, string modelo, int añoFabricacion)
        : base(marca, modelo, añoFabricacion)
    {
    }

    public override void Conducir()
    {
        Console.WriteLine("Conduciendo la motocicleta por la carretera.");
    }
}

class Program
{
    static void Main()
    {
        // Crear instancias de las clases derivadas
        Vehiculo miAutomovil = new Automovil("Firestone", "Toyota", "Corolla", 2022);
        Vehiculo miMotocicleta = new Motocicleta("Honda", "CBR", 2021);

        // Utilizar polimorfismo para llamar al método abstracto
        ConducirVehiculo(miAutomovil);
        ConducirVehiculo(miMotocicleta);
    }

    // Método que utiliza polimorfismo para llamar al método abstracto Conducir
    static void ConducirVehiculo(Vehiculo vehiculo)
    {
        vehiculo.MostrarInformacion();
        vehiculo.Conducir();
        Console.WriteLine();
    }
}
