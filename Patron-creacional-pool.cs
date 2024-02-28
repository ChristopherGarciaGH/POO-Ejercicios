using System;
using System.Collections.Generic;
using System.Data.SqlClient;

// Objeto que representa una conexión a la base de datos
public class DatabaseConnection
{
    public string ConnectionString { get; private set; }
    public SqlConnection Connection { get; private set; }

    internal DatabaseConnection(string connectionString)
    {
        ConnectionString = connectionString;
        Connection = new SqlConnection(connectionString);
        Connection.Open();
        Console.WriteLine("Database connection opened.");
    }

    public void Close()
    {
        if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
        {
            Connection.Close();
            Console.WriteLine("Database connection closed.");
        }
    }
}

// Object Pool para gestionar conexiones a la base de datos
public class DatabaseConnectionPool
{
    private List<DatabaseConnection> _pool;
    private string _connectionString;
    private int _maxPoolSize;

    public DatabaseConnectionPool(string connectionString, int maxPoolSize)
    {
        _connectionString = connectionString;
        _maxPoolSize = maxPoolSize;
        _pool = new List<DatabaseConnection>();
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _maxPoolSize; i++)
        {
            _pool.Add(new DatabaseConnection(_connectionString));
        }
    }

    public DatabaseConnection AcquireConnection()
    {
        if (_pool.Count > 0)
        {
            DatabaseConnection connection = _pool[0];
            _pool.RemoveAt(0);
            Console.WriteLine($"Acquired a database connection. Pool size: {_pool.Count}");
            return connection;
        }
        
        Console.WriteLine("No available connections in the pool. Creating a new one.");
        return new DatabaseConnection(_connectionString);
    }

    public void ReleaseConnection(DatabaseConnection connection)
    {
        if (_pool.Count < _maxPoolSize)
        {
            _pool.Add(connection);
            Console.WriteLine($"Released a database connection back to the pool. Pool size: {_pool.Count}");
        }
        else
        {
            connection.Close();
            Console.WriteLine("Pool is full. Closing the released connection.");
        }
    }
}

class Program
{
    static void Main()
    {
        // Uso del Object Pool para gestionar conexiones a la base de datos
        string connectionString = "your_database_connection_string";
        int maxPoolSize = 5;

        DatabaseConnectionPool connectionPool = new DatabaseConnectionPool(connectionString, maxPoolSize);

        // Realizar operaciones utilizando conexiones adquiridas del Object Pool
        for (int i = 0; i < 8; i++)
        {



            DatabaseConnection connection = connectionPool.AcquireConnection();

            // Realizar operaciones con la conexión (simuladas)
            Console.WriteLine("Executing database operations...");

            // Liberar la conexión después de su uso
            connectionPool.ReleaseConnection(connection);
        }
    }
}
