using System;
using System.Collections.Generic;
using System.Text;

// Interfaz Command
public interface ICommand
{
    void Execute();
    void Undo();
}


// ConcreteCommand para la operación de escribir
public class WriteTextCommand : ICommand
{
    private TextEditor textEditor;
    private string textToAdd;

    public WriteTextCommand(TextEditor textEditor, string textToAdd)
    {
        this.textEditor = textEditor;
        this.textToAdd = textToAdd;
    }

    public void Execute()
    {
        textEditor.AddText(textToAdd);
    }

    public void Undo()
    {
        textEditor.RemoveText(textToAdd);
    }
}


// ConcreteCommand para la operación de deshacer
public class UndoCommand : ICommand
{
    private TextEditor textEditor;

    public UndoCommand(TextEditor textEditor)
    {
        this.textEditor = textEditor;
    }

    public void Execute()
    {
        textEditor.Undo();
    }

    public void Undo()
    {
        // No es necesario implementar Undo para este comando
    }
}

// Receiver - Clase que realiza las acciones reales
public class TextEditor
{
    private StringBuilder text = new StringBuilder();
    private Stack<ICommand> commandHistory = new Stack<ICommand>();
    private Stack<ICommand> undoneCommands = new Stack<ICommand>();

    public void AddText(string textToAdd)
    {
        text.Append(textToAdd);
        Console.WriteLine("Texto agregado: " + textToAdd);
        commandHistory.Push(new WriteTextCommand(this, textToAdd));
    }

    public void RemoveText(string textToRemove)
    {
        int lastIndex = text.ToString().LastIndexOf(textToRemove);
        if (lastIndex != -1)
        {
            text.Remove(lastIndex, textToRemove.Length);
            Console.WriteLine("Texto eliminado: " + textToRemove);
            commandHistory.Push(new WriteTextCommand(this, ""));
        }
    }

    public void Undo()
    {
        if (commandHistory.Count > 0)
        {
            ICommand command = commandHistory.Pop();
            command.Undo();
            undoneCommands.Push(command);
        }
        else
        {
            Console.WriteLine("No hay comandos para deshacer.");
        }
    }

    public void Redo()
    {
        if (undoneCommands.Count > 0)
        {
            ICommand command = undoneCommands.Pop();
            command.Execute();
            commandHistory.Push(command);
        }
        else
        {
            Console.WriteLine("No hay comandos para rehacer.");
        }
    }

    public void PrintText()
    {
        Console.WriteLine("Texto actual: " + text.ToString());
    }
}

// Cliente
class Program
{
    static void Main()
    {
        // Crear instancia del editor de texto
        TextEditor editor = new TextEditor();

        // Crear comandos
        ICommand writeCommand1 = new WriteTextCommand(editor, "Hola, ");
        ICommand writeCommand2 = new WriteTextCommand(editor, "esto es un ejemplo.");
        ICommand undoCommand = new UndoCommand(editor);

        // Ejecutar comandos
        writeCommand1.Execute();
        writeCommand2.Execute();
        editor.PrintText();

        // Deshacer el último comando
        undoCommand.Execute();
        editor.PrintText();

        // Rehacer el comando deshecho
        editor.Redo();
        editor.PrintText();
    }
}







