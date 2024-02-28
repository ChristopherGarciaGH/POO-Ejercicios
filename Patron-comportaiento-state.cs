using System;

// Interfaz que define el comportamiento del estado
public interface IState
{
    void InsertCoin(VendingMachine vendingMachine);
    void PressButton(VendingMachine vendingMachine);
    void Dispense(VendingMachine vendingMachine);
}

// Implementaciones concretas de los estados
public class NoCoinState : IState
{
    public void InsertCoin(VendingMachine vendingMachine)
    {
        Console.WriteLine("Moneda insertada");
        vendingMachine.SetState(new CoinInsertedState());
    }

    public void PressButton(VendingMachine vendingMachine)
    {
        Console.WriteLine("Inserta moneda antes de presionar el botón");
    }

    public void Dispense(VendingMachine vendingMachine)
    {
        Console.WriteLine("Inserta moneda antes de dispensar");
    }
}

public class CoinInsertedState : IState
{
    public void InsertCoin(VendingMachine vendingMachine)
    {
        Console.WriteLine("Ya hay una moneda insertada");
    }

    public void PressButton(VendingMachine vendingMachine)
    {
        Console.WriteLine("Botón presionado, dispensando producto");
        // Lógica de dispensar el producto
        vendingMachine.SetState(new NoCoinState());
    }

    public void Dispense(VendingMachine vendingMachine)
    {
        Console.WriteLine("Presiona el botón antes de dispensar");
    }
}

// Clase principal que utiliza el patrón State
public class VendingMachine
{
    private IState currentState;

    public VendingMachine()
    {
        currentState = new NoCoinState();
    }

    public void SetState(IState state)
    {
        currentState = state;
    }

    public void InsertCoin()
    {
        currentState.InsertCoin(this);
    }

    public void PressButton()
    {
        currentState.PressButton(this);
    }

    public void Dispense()
    {
        currentState.Dispense(this);
    }
}

class Program
{
    static void Main()
    {
        VendingMachine vendingMachine = new VendingMachine();

        vendingMachine.InsertCoin();
        vendingMachine.PressButton();

        vendingMachine.InsertCoin();
        vendingMachine.PressButton();
    }
}
