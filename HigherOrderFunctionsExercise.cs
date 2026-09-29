namespace Assignment3;

// Yagur, "Functional Programming with C#" — Chapter "Higher-Order Functions
// and Delegates" (book's Chapter 6). Exercises 1-3, solved with an original
// example (employee records) instead of the book's tower-defense game.
static class HigherOrderFunctionsExercise
{
    public static void Run()
    {
        Ex1_SortWithDelegate();
        Console.WriteLine();
        Ex2_ActionOnEachItem();
        Console.WriteLine();
        Ex3_FuncComparison();
    }

    public class Employee
    {
        public string Name { get; set; } = "";
        public decimal Salary { get; set; }
        public int YearsOfService { get; set; }
    }

    // -----------------------------------------------------------------
    // Exercise 1 — A higher-order function that sorts a list, with the
    // comparison itself passed in as a delegate.
    // -----------------------------------------------------------------
    public delegate int CompareEmployees(Employee a, Employee b);

    public static void SortEmployees(List<Employee> employees, CompareEmployees compare) =>
        employees.Sort((x, y) => compare(x, y));

    private static void Ex1_SortWithDelegate()
    {
        Console.WriteLine("Exercise 1 — sort with a delegate");
        List<Employee> employees = new()
        {
            new Employee { Name = "Alice", Salary = 72000 },
            new Employee { Name = "Bilal", Salary = 95000 },
            new Employee { Name = "Chen", Salary = 81000 },
        };

        SortEmployees(employees, (a, b) => b.Salary.CompareTo(a.Salary)); // descending

        foreach (Employee e in employees)
        {
            Console.WriteLine($"{e.Name}: {e.Salary:C}");
        }
    }

    // -----------------------------------------------------------------
    // Exercise 2 — A method taking an Action<T> and a list, applying the
    // action to each item.
    // -----------------------------------------------------------------
    public static void ProcessEmployees(List<Employee> employees, Action<Employee> action)
    {
        foreach (Employee e in employees)
        {
            action(e);
        }
    }

    private static void Ex2_ActionOnEachItem()
    {
        Console.WriteLine("Exercise 2 — Action<T> applied to each item");
        List<Employee> employees = new()
        {
            new Employee { Name = "Dana", Salary = 60000 },
            new Employee { Name = "Emeka", Salary = 88000 },
        };

        // Action 1: 5% raise announcement
        ProcessEmployees(employees, e =>
            Console.WriteLine($"{e.Name} would get a raise to {e.Salary * 1.05m:C}"));

        // Action 2: a different calculation, same method, different delegate
        ProcessEmployees(employees, e =>
            Console.WriteLine($"{e.Name} pays roughly {e.Salary * 0.2m:C} in tax"));
    }

    // -----------------------------------------------------------------
    // Exercise 3 — A Func delegate that compares two items and returns
    // one of them.
    // -----------------------------------------------------------------
    public static Employee GetMoreSeniorEmployee(Employee a, Employee b, Func<Employee, Employee, Employee> compare) =>
        compare(a, b);

    private static void Ex3_FuncComparison()
    {
        Console.WriteLine("Exercise 3 — Func<T,T,T> comparison");
        Employee alice = new() { Name = "Alice", YearsOfService = 6 };
        Employee bilal = new() { Name = "Bilal", YearsOfService = 9 };

        Employee moreSenior = GetMoreSeniorEmployee(alice, bilal,
            (a, b) => a.YearsOfService > b.YearsOfService ? a : b);

        Console.WriteLine($"{moreSenior.Name} has been here longer ({moreSenior.YearsOfService} years)");
    }
}
