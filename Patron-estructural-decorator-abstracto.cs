using System;


namespace POO.DecoratorAbstracto
{

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

    // Decorador abstracto
    public abstract class WindowDecorator : IWindow
    {
        protected IWindow decoratedWindow;

        public WindowDecorator(IWindow window)
        {
            decoratedWindow = window;
        }

        public virtual void Draw()
        {
            decoratedWindow.Draw();
        }
    }

    // Decoradores concretos
    public class ScrollableWindow : WindowDecorator
    {
        public ScrollableWindow(IWindow window) : base(window)
        {
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Agregar barra de desplazamiento");
        }
    }

    public class BorderDecorator : WindowDecorator
    {
        public BorderDecorator(IWindow window) : base(window)
        {
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Agregar bordes decorativos");
        }
    }

    public class CloseButtonDecorator : WindowDecorator
    {
        public CloseButtonDecorator(IWindow window) : base(window)
        {
        }

        public override void Draw()
        {
            base.Draw();
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

            // Decorar la ventana decorada con un botón de cierre
            IWindow fullyDecoratedWindow = new CloseButtonDecorator(decoratedWindow);
            Console.WriteLine("\nVentana totalmente decorada:");
            fullyDecoratedWindow.Draw();

            Console.ReadLine();
        }
    }

}