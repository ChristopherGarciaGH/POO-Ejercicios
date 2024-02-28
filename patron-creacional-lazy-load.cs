using System;
using System.Collections.Generic;

// Clase que representa un proyecto con detalles computacionalmente costosos de cargar
public class ProjectDetails
{
    public decimal Budget { get; set; }
    public int DurationInMonths { get; set; }

    public ProjectDetails(decimal budget, int durationInMonths)
    {
        Budget = budget;
        DurationInMonths = durationInMonths;
    }
}

// Clase que representa a un empleado
public class Employee
{
    public string Name { get; set; }
    
    // Detalles del proyecto cargados de forma perezosa
    private Lazy<ProjectDetails> _projectDetails = new Lazy<ProjectDetails>(() => LoadProjectDetailsFromDatabase());

    public Employee(string name)
    {
        Name = name;
    }

    public ProjectDetails ProjectDetails
    {
        get { return _projectDetails.Value; }
    }



    // Método que simula la carga de detalles del proyecto desde una base de datos
    private static ProjectDetails LoadProjectDetailsFromDatabase()
    {
        Console.WriteLine("Loading project details from the database...");
        // Simulación de carga de detalles desde la base de datos
        return new ProjectDetails(budget: 100000, durationInMonths: 12);
    }
}

class Program
{
    static void Main()
    {
        // Creación de un empleado
        Employee employee = new Employee("John Doe");

        // Acceso a los detalles del proyecto (cargados perezosamente)
        Console.WriteLine($"Employee: {employee.Name}");
        Console.WriteLine($"Project Budget: {employee.ProjectDetails.Budget}");
        Console.WriteLine($"Project Duration: {employee.ProjectDetails.DurationInMonths} months");
    }
}
