using System;

// Interfaz que define el comportamiento del algoritmo
public interface IPaymentStrategy
{
    void ProcessPayment(double amount);
}

// Implementaciones concretas de las estrategias
public class CreditCardPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago con tarjeta de crédito por {amount} dólares");
        // Lógica específica de tarjeta de crédito
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago con PayPal por {amount} dólares");
        // Lógica específica de PayPal
    }
}

// Clase principal que utiliza el patrón Strategy
public class ShoppingCart
{
    private IPaymentStrategy paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        paymentStrategy = strategy;
    }

    public void Checkout(double totalAmount)
    {
        // Realizar otras operaciones antes del pago si es necesario
        paymentStrategy.ProcessPayment(totalAmount);
    }
}

// Clase de fábrica para crear instancias de estrategias de pago
public static class PaymentStrategyFactory
{
    public static IPaymentStrategy CreateCreditCardPayment()
    {
        return new CreditCardPayment();
    }

    public static IPaymentStrategy CreatePayPalPayment()
    {
        return new PayPalPayment();
    }
}

class Program
{
    static void Main()
    {
        ShoppingCart cart = new ShoppingCart();

        // Seleccionar la estrategia de pago utilizando el factory method
        IPaymentStrategy creditCardPayment = PaymentStrategyFactory.CreateCreditCardPayment();
        IPaymentStrategy payPalPayment = PaymentStrategyFactory.CreatePayPalPayment();

        // Pagar con tarjeta de crédito
        cart.SetPaymentStrategy(creditCardPayment);
        cart.Checkout(100.00);

        Console.WriteLine();

        // Pagar con PayPal
        cart.SetPaymentStrategy(payPalPayment);
        cart.Checkout(50.00);
    }
}
