// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;

struct Point
{
    // public double x { get; }
    // public double y { get; }
    public double x, y;

    public override string ToString()
    {
        return $"({x}, {y})";
    }

    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

class Rectangle
{
    // private Point top_left { get; }
    // private Point bottom_right { get; }
    private Point top_left, bottom_right;

    public override string ToString()
    {
        return $"Rect {{ {nameof(top_left)}: {top_left}, {nameof(bottom_right)}: {bottom_right} }}";
    }

    public Rectangle(Point top_left, Point bottom_right)
    {
        this.top_left = top_left;
        this.bottom_right = bottom_right;
    }

    public void Enclose(Rectangle rect)
    {
        top_left.x = Math.Min(top_left.x, rect.top_left.x);
        top_left.y = Math.Max(top_left.y, rect.top_left.y);
        bottom_right.x = Math.Max(bottom_right.x, rect.bottom_right.x);
        bottom_right.y = Math.Min(bottom_right.y, rect.bottom_right.y);
    }
    
    public static Rectangle EncloseBoth(Rectangle first, Rectangle second)
    {
        return new Rectangle(
            new Point(
                Math.Min(first.top_left.x, second.top_left.x),
                Math.Max(first.top_left.y, second.top_left.y)),
            new Point(
                Math.Max(first.bottom_right.x, second.bottom_right.x),
                Math.Min(first.bottom_right.y, second.bottom_right.y))
        );
    }
}



internal class Program
{
    private static double GetRandomDouble(int fromInclusive, int toExclusive)
    {
        return Convert.ToDouble(RandomNumberGenerator.GetInt32(fromInclusive * 1000, toExclusive * 1000)) / 1000;
    }
    
    public static void Main(string[] args)
    {
        const double max_x = 1000, max_y = 1000;
        const int rect_count = 500;
        
        var max_x_int = Convert.ToInt32(Math.Round(max_x));
        var max_y_int = Convert.ToInt32(Math.Round(max_y));

        var rectangles = new Rectangle[rect_count];
        for (var i = 0; i < rect_count; i++)
        {
            rectangles[i] = new Rectangle(
                new Point(
                    GetRandomDouble(-max_x_int, max_x_int),
                    GetRandomDouble(-max_y_int, max_y_int)),
                new Point(
                    GetRandomDouble(-max_x_int, max_x_int),
                    GetRandomDouble(-max_y_int, max_y_int))
                );
        }
        
        var enclosing_rect = rectangles[0];

        foreach (var rect in rectangles)
        {
            enclosing_rect.Enclose(rect);
        }
        
        Console.WriteLine($"Rectangle enclosing all others: {enclosing_rect}");
    }
}
