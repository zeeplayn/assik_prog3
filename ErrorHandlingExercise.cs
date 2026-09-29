namespace Assignment3;

// Yagur, "Functional Programming with C#" — Chapter "Error Handling"
// (book's Chapter 5). Exercises 1-3, solved with an original example
// (an order/payment workflow) instead of the book's tower-defense game.
static class ErrorHandlingExercise
{
    public static void Run()
    {
        Ex1_ResultType();
        Console.WriteLine();
        Ex2_RailwayOrientedProgramming();
        Console.WriteLine();
        Ex3_RetryMechanism();
    }

    // -----------------------------------------------------------------
    // Exercise 1 — Refactor a bool-returning function to return a
    // Result type with a custom error instead.
    // -----------------------------------------------------------------
    // "Before" version:
    //   public bool ChargeCard(decimal amount)
    //   {
    //       if (/* payment fails */) return false;
    //       return true;
    //   }
    public enum PaymentError
    {
        InsufficientFunds,
        CardDeclined,
        CardExpired
    }

    public static Result<bool, PaymentError> ChargeCard(decimal amount, decimal availableCredit, bool cardExpired)
    {
        if (cardExpired)
        {
            return Result<bool, PaymentError>.Fail(PaymentError.CardExpired);
        }
        if (amount > availableCredit)
        {
            return Result<bool, PaymentError>.Fail(PaymentError.InsufficientFunds);
        }
        return Result<bool, PaymentError>.Ok(true);
    }

    private static void Ex1_ResultType()
    {
        Console.WriteLine("Exercise 1 — Result<T, TError> instead of bool");
        Result<bool, PaymentError> result = ChargeCard(150m, availableCredit: 100m, cardExpired: false);
        Console.WriteLine(result.IsSuccess
            ? "Payment succeeded"
            : $"Payment failed: {result.Error}");
    }

    // -----------------------------------------------------------------
    // Exercise 2 — Refactor a nested if-chain into a Railway-Oriented
    // Programming (ROP) pipeline using .Bind().
    // -----------------------------------------------------------------
    public record Order(int Id, decimal Amount);
    public enum OrderError { InvalidData, ValidationFailed, PaymentFailed }

    public static Result<Order, OrderError> ParseOrder(string rawData) =>
        decimal.TryParse(rawData, out decimal amount) && amount > 0
            ? Result<Order, OrderError>.Ok(new Order(1, amount))
            : Result<Order, OrderError>.Fail(OrderError.InvalidData);

    public static Result<Order, OrderError> ValidateOrder(Order order) =>
        order.Amount <= 10_000m
            ? Result<Order, OrderError>.Ok(order)
            : Result<Order, OrderError>.Fail(OrderError.ValidationFailed);

    public static Result<Order, OrderError> ProcessPayment(Order order) =>
        Result<Order, OrderError>.Ok(order); // pretend payment always succeeds here

    private static void Ex2_RailwayOrientedProgramming()
    {
        Console.WriteLine("Exercise 2 — Railway-Oriented Programming pipeline");
        Result<Order, OrderError> result = ParseOrder("250.00")
            .Bind(ValidateOrder)
            .Bind(ProcessPayment);

        Console.WriteLine(result.IsSuccess
            ? $"Order #{result.Value!.Id} for {result.Value.Amount:C} processed"
            : $"Order processing failed: {result.Error}");
    }

    // -----------------------------------------------------------------
    // Exercise 3 — Retry mechanism for a flaky operation, returning a
    // Result instead of throwing or looping forever.
    // -----------------------------------------------------------------
    private static readonly Random _random = new();

    private static bool CallFlakyPaymentGateway() => _random.Next(3) == 0; // ~33% success

    public static Result<bool, string> TryChargeWithRetries(int maxRetries)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            if (CallFlakyPaymentGateway())
            {
                return Result<bool, string>.Ok(true);
            }
        }
        return Result<bool, string>.Fail($"Payment gateway failed after {maxRetries} attempts.");
    }

    private static void Ex3_RetryMechanism()
    {
        Console.WriteLine("Exercise 3 — retry mechanism");
        Result<bool, string> result = TryChargeWithRetries(maxRetries: 5);
        Console.WriteLine(result.IsSuccess ? "Payment succeeded" : result.Error);
    }
}

// --- Small, generic Result<TValue, TError> used by all three exercises ---
public class Result<TValue, TError>
{
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public TError? Error { get; }

    private Result(bool isSuccess, TValue? value, TError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<TValue, TError> Ok(TValue value) => new(true, value, default);
    public static Result<TValue, TError> Fail(TError error) => new(false, default, error);

    public Result<TNext, TError> Bind<TNext>(Func<TValue, Result<TNext, TError>> next) =>
        IsSuccess ? next(Value!) : Result<TNext, TError>.Fail(Error!);
}
