using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Originator o sujeto: el objeto cuyo estado queremos guardar y restaurar
class Editor
{
    private string content;

    public string Content
    {
        get { return content; }
        set
        {
            Console.WriteLine("Editor: Modificando el contenido.");
            content = value;
        }
    }

    // Crea un Memento que captura el estado actual del Editor
    public EditorMemento CreateMemento()
    {
        Console.WriteLine("Editor: Creando Memento.");
        return new EditorMemento(content);
    }

    // Restaura el estado del Editor a partir de un Memento
    public void RestoreMemento(EditorMemento memento)
    {
        Console.WriteLine("Editor: Restaurando desde Memento.");
        content = memento.SavedContent;
    }
}

// Memento: almacena el estado del Editor. Es  la clase que representa el estado interno del sujeto. La instantanea
class EditorMemento
{
    public string SavedContent { get; }

    

    public EditorMemento(string content)
    {
        SavedContent = content;
    }
}

// Manejador del historial de mementos: mantiene y gestiona los Mementos
class History
{
    private List<EditorMemento> mementos = new List<EditorMemento>();

    public void AddMemento(EditorMemento memento)
    {
        Console.WriteLine("Historia: Agregando Memento a la lista.");
        mementos.Add(memento);
    }

    public EditorMemento GetMemento(int index)
    {
        Console.WriteLine("Historia: Obteniendo Memento de la lista.");
        return mementos[index];
    }
}

class Program
{
    static void Main()
    {
        // Crear un editor
        var editor = new Editor();
        var history = new History();

        // Modificar el contenido y guardar el estado en la historia
        editor.Content = "Versión 1";
        history.AddMemento(editor.CreateMemento());

        // Modificar el contenido nuevamente
        editor.Content = "Versión 2";

        // Restaurar a la versión anterior desde la historia
        editor.RestoreMemento(history.GetMemento(0));

        Console.WriteLine("Contenido actual del editor: " + editor.Content);
    }
}
