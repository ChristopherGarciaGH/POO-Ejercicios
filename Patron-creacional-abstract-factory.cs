using System;

// Interfaz para la fábrica abstracta que define métodos para crear familias de productos
public interface ICarFactory
{
    IEngine CreateEngine();
    IBody CreateBody();
    IInterior CreateInterior();
}

// Interfaz para el producto "Motor"
public interface IEngine
{
    void Start();
}

// Interfaz para el producto "Carrocería"
public interface IBody
{
    void Assemble();
}

// Interfaz para el producto "Interior"
public interface IInterior
{
    void Install();
}

// Implementación concreta de la fábrica para producir componentes de automóviles de lujo
public class LuxuryCarFactory : ICarFactory
{
    public IEngine CreateEngine()
    {
        return new LuxuryEngine();
    }

    public IBody CreateBody()
    {
        return new LuxuryBody();
    }

    public IInterior CreateInterior()
    {
        return new LuxuryInterior();
    }
}

// Implementación concreta de la fábrica para producir componentes de automóviles estándar
public class StandardCarFactory : ICarFactory
{
    public IEngine CreateEngine()
    {
        return new StandardEngine();
    }

    public IBody CreateBody()
    {
        return new StandardBody();
    }

    public IInterior CreateInterior()
    {
        return new StandardInterior();
    }
}

// Implementaciones concretas de productos para automóviles de lujo
public class LuxuryEngine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Luxury Engine started smoothly.");
    }
}

public class LuxuryBody : IBody
{
    public void Assemble()
    {
        Console.WriteLine("Luxury Body assembled with precision.");
    }
}

public class LuxuryInterior : IInterior
{
    public void Install()
    {
        Console.WriteLine("Luxury Interior installed with attention to detail.");
    }
}

// Implementaciones concretas de productos para automóviles estándar
public class StandardEngine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Standard Engine started.");
    }
}

public class StandardBody : IBody
{
    public void Assemble()
    {
        Console.WriteLine("Standard Body assembled.");
    }
}

public class StandardInterior : IInterior
{
    public void Install()
    {
        Console.WriteLine("Standard Interior installed.");
    }
}

// Cliente que utiliza la fábrica abstracta para crear componentes de automóviles
public class CarClient
{
    private readonly ICarFactory _carFactory;

    public CarClient(ICarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    public void BuildCar()
    {
        IEngine engine = _carFactory.CreateEngine();
        IBody body = _carFactory.CreateBody();
        IInterior interior = _carFactory.CreateInterior();

        Console.WriteLine("Building a car with the following components:");
        engine.Start();
        body.Assemble();
        interior.Install();

        Console.WriteLine("Car built successfully.");
    }
}

class Program
{
    static void Main()
    {
        // Cliente que utiliza la fábrica abstracta para crear un automóvil de lujo
        CarClient luxuryCarClient = new CarClient(new LuxuryCarFactory());
        luxuryCarClient.BuildCar();

        Console.WriteLine();

        // Cliente que utiliza la fábrica abstracta para crear un automóvil estándar
        CarClient standardCarClient = new CarClient(new StandardCarFactory());
        standardCarClient.BuildCar();
    }
}
