using System;

// Clase que representa la funcionalidad de encendido y apagado
public class EncendidoApagado
{
    private bool encendido;

    public void Encender()
    {
        encendido = true;
        Console.WriteLine("El televisor está encendido.");
    }

    public void Apagar()
    {
        encendido = false;
        Console.WriteLine("El televisor está apagado.");
    }
}

// Clase que representa la funcionalidad de ajuste de volumen
public class AjusteVolumen
{
    private int volumen;

    public void SubirVolumen()
    {
        volumen++;
        Console.WriteLine($"Volumen subido a {volumen}.");
    }

    public void BajarVolumen()
    {
        volumen = Math.Max(0, volumen - 1);
        Console.WriteLine($"Volumen bajado a {volumen}.");
    }
}

// ATENCIÓN!!! ESTO ES LO FUNDAMENTAL:  Clase principal que utiliza composición para agregar funcionalidades!!
// también podrías hacer composición con abstracciones. Más potente. (interfaces, IVolumenAjustable por ejemplo) en lugar de tipos concretos (AjusteVolumen por ejemplo).
// de esta forma con una abstracción podrias tener distintos comportamientos en tiempo de ejecución: ajuste por voz, ajuste por mando a distancia
public class Televisor
{
    private EncendidoApagado encendidoApagado = new EncendidoApagado();
    private AjusteVolumen ajusteVolumen = new AjusteVolumen();

    public void Encender()
    {
        encendidoApagado.Encender();
    }

    public void Apagar()
    {
        encendidoApagado.Apagar();
    }

    public void SubirVolumen()
    {
        ajusteVolumen.SubirVolumen();
    }

    public void BajarVolumen()
    {
        ajusteVolumen.BajarVolumen();
    }
}

class Program
{
    static void Main()
    {
        Televisor miTelevisor = new Televisor();

        miTelevisor.Encender();
        miTelevisor.SubirVolumen();
        miTelevisor.BajarVolumen();
        miTelevisor.Apagar();
    }
}
