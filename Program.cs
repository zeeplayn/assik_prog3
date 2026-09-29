using Assignment3;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Assignment 3 — choose an exercise to run:");
    Console.WriteLine("1) Price Ch.5 Ex.5.1-5.2 — OOP, access modifiers");
    Console.WriteLine("2) Yagur 'Error Handling' Ex.1-3 — Result type, ROP, retry");
    Console.WriteLine("3) Yagur 'Higher-Order Functions and Delegates' Ex.1-3");
    Console.WriteLine("A) Run all");
    Console.WriteLine("Q) Quit");
    Console.Write("> ");

    string? choice = Console.ReadLine()?.Trim().ToUpperInvariant();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            OopExercise.Run();
            break;
        case "2":
            ErrorHandlingExercise.Run();
            break;
        case "3":
            HigherOrderFunctionsExercise.Run();
            break;
        case "A":
            OopExercise.Run();
            Console.WriteLine();
            ErrorHandlingExercise.Run();
            Console.WriteLine();
            HigherOrderFunctionsExercise.Run();
            break;
        case "Q":
            return;
        default:
            Console.WriteLine("Unknown option, try again.");
            break;
    }
}
