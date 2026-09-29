namespace Ch06Ex02Inheritance;

public class Rectangle : Shape
{
    public Rectangle(double height, double width)
    {
        Height = height;
        Width = width;
    }

    public override double Area => Height * Width;
}

public class Square : Shape
{
    public Square(double side)
    {
        Height = side;
        Width = side;
    }

    public override double Area => Height * Width;
}

public class Circle : Shape
{
    public double Radius { get; }

    public Circle(double radius)
    {
        Radius = radius;
        Height = radius * 2;
        Width = radius * 2;
    }

    public override double Area => Math.PI * Radius * Radius;
}
