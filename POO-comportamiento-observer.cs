using System;
using System.Collections.Generic;

// Interfaz del observador
public interface INewsObserver
{
    void Update(string news);
}

// Sujeto (Observable)
public class AgenciaEfeNoticiasService
{
    private List<INewsObserver> observers = new List<INewsObserver>();
    
    public void AddObserver(INewsObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(INewsObserver observer)
    {
        observers.Remove(observer);
    }

    public void PublishNews(string news)
    {
        Console.WriteLine($"Nueva noticia: {news}");
        NotifyObservers(news);
    }

    private void NotifyObservers(string news)
    {
        foreach (var observer in observers)
        {
            observer.Update(news);
        }
    }
}

// Implementación de un observador
public class A3Reader : INewsObserver
{
    private string readerName;

    public A3Reader(string name)
    {
        readerName = name;
    }

    public void Update(string news)
    {
        // La lógica para recibir la noticia propia del observador. La que sea

        Console.WriteLine($"{readerName} recibió la noticia: {news}");
    }
}

// Implementación de un observador
public class CadenaSerReader : INewsObserver
{
    private string readerName;

    public CadenaSerReader(string name)
    {
        readerName = name;
    }

    public void Update(string news)
    {
        // La lógica para recibir la noticia propia del observador. La que sea

        Console.WriteLine($"{readerName} recibió la noticia: {news}");
    }
}

class Program
{
    static void Main()
    {

        List<string> list = new List<string>(); 
        HashSet<string> set = new HashSet<string>();

        foreach (var item in list)
        { 
        
        }

        INewsObserver observador1 = new A3Reader();
        INewsObserver observador2 = new CadenaSerReader();


        observador1.Update();

        


        // Crear el servicio de noticias
        var newsService = new AgenciaEfeNoticiasService();

        // Crear observadores (lectores de noticias)
        INewsObserver reader1 = new A3Reader("A3 Noticias");
        CadenaSerReader reader2 = new CadenaSerReader("Cadena Ser Hora 25");

        // Registrar observadores en el servicio de noticias
        newsService.AddObserver(reader1);
        newsService.AddObserver(reader2);

        // Publicar una noticia (notificar a los observadores)
        newsService.PublishNews("Importante: Nuevo descubrimiento científico!");

        // Desregistrar un observador
        newsService.RemoveObserver(reader1);

        // Publicar otra noticia (notificar solo al observador restante)
        newsService.PublishNews("Actualización: Cambios en la bolsa de valores");

        Console.ReadLine();
    }
}
