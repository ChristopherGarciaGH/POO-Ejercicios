using System;

// Clase de alto nivel que depende de una implementación concreta de bajo nivel
public class Logger
{
    public void Log(string message)
    {
        Console.WriteLine($"Logging: {message}");
    }
}

// Clase de bajo nivel
public class FileLogger
{
    public void LogToFile(string message)
    {
        Console.WriteLine($"Logging to file: {message}");
    }
}

// Clase de alto nivel que depende de clases concretas de bajo nivel
public class UserManager
{
    private Logger logger;

    public UserManager()
    {
        logger = new Logger(); // Creando una instancia concreta de Logger
    }

    public void RegisterUser(string username)
    {
        // Lógica de registro de usuario
        logger.Log($"User '{username}' registered.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        UserManager userManager = new UserManager();
        userManager.RegisterUser("JohnDoe");
    }
}
