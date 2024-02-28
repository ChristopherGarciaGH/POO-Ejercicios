using System;

// Componente base
public interface IWindow
{
    void Draw();
}

// Implementación del componente base
public class SimpleWindow : IWindow
{
    public void Draw()
    {
        Console.WriteLine("Dibujar ventana simple");
    }
}

// Decoradores concretos
public class ScrollableWindow : IWindow
{
    private readonly IWindow decoratedWindow;

    public ScrollableWindow(IWindow window)
    {
        decoratedWindow = window;
    }

    public void Draw()
    {
        decoratedWindow.Draw();
        Console.WriteLine("Agregar barra de desplazamiento");
    }
}

public class BorderDecorator : IWindow
{
    private readonly IWindow decoratedWindow;

    public BorderDecorator(IWindow window)
    {
        decoratedWindow = window;
    }

    public void Draw()
    {
        decoratedWindow.Draw();
        Console.WriteLine("Agregar bordes decorativos");
    }
}

public class CloseButtonDecorator : IWindow
{
    private readonly IWindow decoratedWindow;

    public CloseButtonDecorator(IWindow window)
    {
        decoratedWindow = window;
    }

    public void Draw()
    {
        decoratedWindow.Draw();
        Console.WriteLine("Agregar botón de cierre");
    }
}

class Program
{
    static void Main()
    {
        // Crear una ventana simple
        IWindow simpleWindow = new SimpleWindow();
        Console.WriteLine("Ventana simple:");
        simpleWindow.Draw();



        // Decorar la ventana con barra de desplazamiento y bordes
        IWindow decoratedWindow = new ScrollableWindow(new BorderDecorator(simpleWindow));
        Console.WriteLine("\nVentana decorada:");
        decoratedWindow.Draw();

        // Salida por consola sería:
        // Dibujar ventana simple
        // Agregar bordes decorativos
        // Agregar barra de desplazamiento

        // Decorar la ventana decorada con un botón de cierre
        IWindow fullyDecoratedWindow = new CloseButtonDecorator(decoratedWindow);
        Console.WriteLine("\nVentana totalmente decorada:");
        fullyDecoratedWindow.Draw();

        Console.ReadLine();
    }
}
