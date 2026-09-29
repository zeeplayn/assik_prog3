using Ch06Ex02Inheritance;

Rectangle r = new(height: 3, width: 4.5);
Console.WriteLine($"Rectangle H: {r.Height}, W: {r.Width}, Area: {r.Area}");

Square s = new(5);
Console.WriteLine($"Square H: {s.Height}, W: {s.Width}, Area: {s.Area}");

Circle c = new(radius: 2.5);
Console.WriteLine($"Circle H: {c.Height}, W: {c.Width}, Area: {c.Area}");
