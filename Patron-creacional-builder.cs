using System;

// Producto: Coche
class Coche
{
    public string Modelo { get; set; }
    public string Motor { get; set; }
    public int NumeroDePuertas { get; set; }
    public string TipoDeCarroceria { get; set; }
    public string SistemaDeEntretenimiento { get; set; }
    public string TipoDeCombustible { get; set; }
    public int PotenciaElectrica { get; set; }

    public void Mostrar()
    {
        Console.WriteLine($"Detalles del Coche:");
        Console.WriteLine($"Modelo: {Modelo}");
        Console.WriteLine($"Motor: {Motor}");
        Console.WriteLine($"Número de Puertas: {NumeroDePuertas}");
        Console.WriteLine($"Tipo de Carrocería: {TipoDeCarroceria}");
        Console.WriteLine($"Sistema de Entretenimiento: {SistemaDeEntretenimiento}");
        if (Motor == "Combustión Interna")
        {
            Console.WriteLine($"Tipo de Combustible: {TipoDeCombustible}");
        }
        else if (Motor == "Eléctrico")
        {
            Console.WriteLine($"Potencia Eléctrica: {PotenciaElectrica} kW");
        }
    }
}

// Builder: Interfaz para construir el coche
interface ICocheBuilder
{
    ICocheBuilder AddModelo(string modelo);
    ICocheBuilder AddMotorCombustion(string tipoMotorCombustion, string tipoCombustible);
    ICocheBuilder AddMotorElectrico(int potenciaElectrica);
    ICocheBuilder AddNumeroDePuertas(int numeroDePuertas);
    ICocheBuilder AddTipoDeCarroceria(string tipoDeCarroceria);
    ICocheBuilder AddSistemaDeEntretenimiento(string sistemaDeEntretenimiento);
    Coche Build();
}

// ConcreteBuilder: Implementación del builder
class CocheBuilder : ICocheBuilder
{
    private Coche coche;

    public CocheBuilder()
    {
        coche = new Coche();
    }

    public ICocheBuilder AddModelo(string modelo)
    {
        coche.Modelo = modelo;
        return this;
    }

    public ICocheBuilder AddMotorCombustion(string tipoMotorCombustion, string tipoCombustible)
    {
        coche.Motor = tipoMotorCombustion;
        coche.TipoDeCombustible = tipoCombustible;
        return this;
    }

    public ICocheBuilder AddMotorElectrico(int potenciaElectrica)
    {
        coche.Motor = "Eléctrico";
        coche.PotenciaElectrica = potenciaElectrica;
        return this;
    }

    public ICocheBuilder AddNumeroDePuertas(int numeroDePuertas)
    {
        coche.NumeroDePuertas = numeroDePuertas;
        return this;
    }

    public ICocheBuilder AddTipoDeCarroceria(string tipoDeCarroceria)
    {
        coche.TipoDeCarroceria = tipoDeCarroceria;
        return this;
    }

    public ICocheBuilder AddSistemaDeEntretenimiento(string sistemaDeEntretenimiento)
    {
        coche.SistemaDeEntretenimiento = sistemaDeEntretenimiento;
        return this;
    }

    public Coche Build(bool grabar)
    {
        return coche;
    }
}

// Director: Construye el coche usando el builder
class Concesionario
{
    public Coche ConstruirCoche(ICocheBuilder cocheBuilder)
    {
        return cocheBuilder.Build();
    }
}

// Cliente
class Program
{
    static void Main()
    {
        // Crear el builder específico y construir el coche usando la concatenación de métodos
        Coche cochePersonalizado = new CocheBuilder()
            .AddModelo("Modelo Deportivo")
            .AddMotorCombustion("Motor de Combustión Interna", "Gasolina")
            .AddNumeroDePuertas(2)
            .AddTipoDeCarroceria("Coupé")
            .AddSistemaDeEntretenimiento("Sistema de Sonido Premium")
            .Build();

        Coche cocheElectrico = new CocheBuilder()
            .AddModelo("Modelo Eléctrico")
            .AddMotorElectrico(150)
            .AddNumeroDePuertas(4)
            .AddTipoDeCarroceria("Sedán")
            .AddSistemaDeEntretenimiento("Sistema de Sonido Básico")
            .Build();

        // Mostrar los coches construidos
        cochePersonalizado.Mostrar();
        Console.WriteLine();
        cocheElectrico.Mostrar();
    }
}
