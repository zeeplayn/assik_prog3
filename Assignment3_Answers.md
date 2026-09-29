# Assignment 3 — Answers

## Page ranges

- **Price, pp. 250–385** → Chapter 5 "Building Your Own Types with Object-Oriented Programming" (231–292, Exercises on p. 289 — included) in full, Chapter 6 "Implementing Interfaces and Inheriting Classes" (293–366, Exercises on p. 364 — included) in full, plus the start of Chapter 7 (367–385), but its exercises on p. 412 are **not** included in the range.
- **Yagur, pp. 104–127** → this time, no surprises: this is the end of the **"Error Handling"** chapter (Exercises on pp. 105–108 — fall entirely within the start of the range) and the whole of the **"Higher-Order Functions and Delegates"** chapter (109–126, Exercises on p. 123 — also included). In the course's numbering (which skips the intro chapter), this corresponds to "Chapter 5" — matching what you wrote.

---

## Book 1 (Price) — Chapter 5 "Building Your Own Types with Object-Oriented Programming"

### Exercise 5.1 — Test your knowledge

1. **The seven access modifiers** — `public` (visible everywhere), `private` (only within its own type), `protected` (own type + derived types), `internal` (only within the assembly), `protected internal` (own type/derived types OR within the assembly — union), `private protected` (own type/derived types AND within the assembly — intersection), `file` (only within the current file, C# 11+).
2. **static vs const vs readonly** — `static` — the member belongs to the type, not an instance; `const` — the value is known at compile time and never changes (implicitly static); `readonly` — the value can only be set in the constructor (or at declaration) and cannot change afterward, but unlike `const`, it can be computed at runtime.
3. **What a constructor does** — initializes a new instance of a type (sets initial values for fields/properties) at the moment the object is created with `new`.
4. **The `[Flags]` attribute** — indicates that an enum's values are a combination of bit flags that can be combined with `|`; it also improves the output of `ToString()` (it will list all combined flags separated by commas, instead of just a number).
5. **`partial`** — lets you split a class/struct/interface declaration across multiple files (for example, to separate generated code from hand-written code); the compiler merges them into one type.
6. **A tuple** — a lightweight structure for grouping several values without creating a separate class, e.g. `(string Name, int Age) person = ("Alice", 30);`.
7. **The `record` keyword** — creates an (by default) immutable reference type with automatically generated value-based equality (`Equals`/`GetHashCode`), `ToString()`, and support for `with`-expressions to create copies with modified fields.
8. **Overloading** — several methods with the same name but different parameter lists (by count or type) within the same type.
9. **`public List<Person> Children = new();` vs `public List<Person> Children => new();`** — the first is a field/auto-property backed by a field initialized once when the object is created (the list is created once and reused); the second is an expression-bodied property (effectively a getter method) that creates a **new**, empty list on **every** access to `Children` — meaning anything previously added is lost.
10. **Making a method parameter optional** — give it a default value: `void Foo(int x, int y = 10)`.

### Exercise 5.2 — Practice with access modifiers

Covered in the code (`OopExercise.cs`): the `Car` class has no modifier, so it's implicitly `internal` and therefore invisible from another project; the `Start()` method is marked `internal`, so it's also invisible outside the assembly. The compiler in the console app project would raise accessibility errors on both. The fix is to make the class and whichever members the console app needs `public`.

### Exercise 5.3 — Explore

Link to `book-links.md#chapter-5` in the author's repository — extra material on OOP.

---

## Book 1 (Price) — Chapter 6 "Implementing Interfaces and Inheriting Classes"

### Exercise 6.1 — Test your knowledge

1. **Delegate** — a type describing a method's signature, letting you pass methods around as values (in variables, as parameters).
2. **Event** — a delegate-based member implementing the publisher-subscriber pattern: it lets a type notify subscribers that something has happened.
3. **Base and derived classes** — a derived class inherits the base class's members via `:`; it can access base members directly (if `public`/`protected`) or through the `base` keyword.
4. **`is` vs `as`** — `is` checks type compatibility and returns a `bool` (can be combined with pattern matching: `if (x is Cat cat)`); `as` attempts a cast and returns `null` if it fails (instead of throwing an exception).
5. **`sealed`** — prevents a class from being derived from further, or a method from being overridden any further.
6. **Preventing instantiation via `new`** — make the constructor `private` (or make the class `static`/`abstract`, depending on the goal).
7. **Allowing a member to be overridden** — the `virtual` modifier (on the base class); overridden with `override`.
8. **Destructor vs Deconstruct** — a destructor (`~ClassName()`) is invoked by the garbage collector before an object's memory is reclaimed; a `Deconstruct` method is a way to "unpack" an object into its parts via the pattern `var (a, b) = obj;` — it has nothing to do with garbage collection.
9. **Constructor signatures every exception should have** — four standard ones: parameterless; with `string message`; with `string message, Exception innerException`; and the protected serialization constructor `(SerializationInfo info, StreamingContext context)`.
10. **Extension method** — a static method in a static class whose first parameter is marked with `this`, letting you call it as if it were a method of an existing type: `public static bool IsValid(this string s) => ...`.

### Exercise 6.2 — Inheritance hierarchy (Shape/Rectangle/Square/Circle)

Since the exercise explicitly asks for a **new console app named `Ch06Ex02Inheritance`**, it's provided as a separate project (see the files below), with the exact set of classes and output specified in the exercise.

### Exercises 6.3–6.4 — Explore

- **6.3** — read the online-only section about code analyzers (`ch06-writing-better-code.md` in the author's repository).
- **6.4** — link to `book-links.md#chapter-6`.

---

## Book 2 (Yagur) — "Error Handling" — Exercises 1–3

Working code in `ErrorHandlingExercise.cs` (original example — order payment processing, instead of the book's tower-defense game):

- **Exercise 1** — the card-charging method, instead of returning `bool`, returns a small custom `Result<TValue, TError>` carrying a specific failure reason (`InsufficientFunds`, `CardDeclined`, `CardExpired`).
- **Exercise 2** — the "parse → validate → process payment" chain is rewritten using Railway-Oriented Programming (`.Bind(...)`) instead of nested `if` statements.
- **Exercise 3** — a retry mechanism: `TryChargeWithRetries` calls a "flaky" payment gateway a given number of times before returning a failed `Result`.

## Book 2 (Yagur) — "Higher-Order Functions and Delegates" — Exercises 1–3

Working code in `HigherOrderFunctionsExercise.cs` (original example — employees, instead of towers/enemies):

- **Exercise 1** — sorting a list using a custom delegate `CompareEmployees`, passed in as a parameter.
- **Exercise 2** — a method taking an `Action<Employee>` and applying it to every item in a list (tested with two different Actions).
- **Exercise 3** — a method taking a `Func<Employee, Employee, Employee>` to compare two employees and return the "more suitable" one.

---

## File structure

- **`Assignment3`** — a single console project with a menu: `OopExercise.cs` (5.1–5.2), `ErrorHandlingExercise.cs`, `HigherOrderFunctionsExercise.cs`.
- **`Ch06Ex02Inheritance`** — a separate console project for Exercise 6.2, as the exercise requires (exact project name).
- **`Assignment3.sln`** — open this file in Visual Studio so both projects load correctly, each in its own isolation (avoiding the earlier "Only one compilation unit can have top-level statements" error).
