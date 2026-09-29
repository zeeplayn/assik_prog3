namespace Assignment3;

// Price, "C# 12 and .NET 8" — Chapter 5, "Building Your Own Types with OOP"
// Exercise 5.1 (knowledge questions) is answered in the write-up.
// Exercise 5.2 — Practice with access modifiers: this is a "spot the compiler
// error" exercise, so instead of code that fails to build, this file
// demonstrates the fix, with comments explaining what was wrong.
static class OopExercise
{
    public static void Run()
    {
        Console.WriteLine("Exercise 5.2 — access modifiers");
        Console.WriteLine();
        Console.WriteLine("Original Car.cs:");
        Console.WriteLine("  class Car                 // internal by default");
        Console.WriteLine("  {");
        Console.WriteLine("      int Wheels { get; set; }        // private by default");
        Console.WriteLine("      public bool IsEV { get; set; }");
        Console.WriteLine("      internal void Start() { ... }   // only visible inside this assembly");
        Console.WriteLine("  }");
        Console.WriteLine();
        Console.WriteLine("Compiler errors this would produce from another project's Program.cs:");
        Console.WriteLine(" - 'Car' is inaccessible due to its protection level");
        Console.WriteLine("   (the class itself has no access modifier, so it defaults to 'internal'");
        Console.WriteLine("   and cannot be seen outside its own class library project).");
        Console.WriteLine(" - 'Car.Start()' is inaccessible due to its protection level");
        Console.WriteLine("   ('internal' members are only visible within the same assembly).");
        Console.WriteLine(" - Note: 'Wheels' would ALSO be inaccessible (it's implicitly private),");
        Console.WriteLine("   but the sample Program.cs never touches it, so the compiler doesn't");
        Console.WriteLine("   flag it — it would only show up if something tried to use it.");
        Console.WriteLine();
        Console.WriteLine("Fix: make the class and the members the console app needs 'public':");

        Car fiat = new() { IsEV = true };
        fiat.Start();
    }
}

// Fixed version — public class, public members that need to be used
// from the referencing console app project.
public class Car
{
    private int Wheels { get; set; } = 4; // still private: an implementation detail
    public bool IsEV { get; set; }
    public void Start() => Console.WriteLine("Starting...");
}
