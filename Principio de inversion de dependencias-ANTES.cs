using System;

// Interfaz que define el contrato para un logger
public interface ILogger
{
    void Log(string message);
}

// Implementación concreta de ILogger para logging en la consola
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Logging: {message}");
    }
}

// Implementación concreta de ILogger para logging en un archivo
public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Logging to file: {message}");
    }
}

// Clase de alto nivel que depende de una abstracción (ILogger) en lugar de una implementación concreta
public class UserManager
{
    private ILogger logger;

    // Inyección de dependencia a través del constructor
    public UserManager(ILogger logger)
    {
        this.logger = logger;
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
        // Creando una instancia de UserManager con un ConsoleLogger inyectado
        UserManager userManagerWithConsoleLogger = new UserManager(new ConsoleLogger());
        userManagerWithConsoleLogger.RegisterUser("JohnDoe");

        // Creando una instancia de UserManager con un FileLogger inyectado
        UserManager userManagerWithFileLogger = new UserManager(new FileLogger());
        userManagerWithFileLogger.RegisterUser("JaneDoe");
    }
}
