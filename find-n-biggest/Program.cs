// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;

internal class Program
{
    public static void Main(string[] args)
    {
        const uint N = 5;
        const uint array_length = 20;

        Console.Write("Array: [ ");
        var array = new uint[array_length];
        for (uint i = 0; i < array_length; i++)
        {
            array[i] = Convert.ToUInt32(RandomNumberGenerator.GetInt32(0, 1000));
            Console.Write($"{array[i]} ");
        }
        Console.WriteLine("]");

        var biggest = new SortedSet<uint>();
        // var biggest = new uint[N];
        for (uint i = 0; i < array_length; i++)
        {
            if ((i < N) || (array[i] > biggest.Min))
            {
                // Console.WriteLine($"+ {array[i]}");
                biggest.Add(array[i]);

                if (i >= N)
                {
                    // Console.WriteLine($"- {biggest.Min}");
                    biggest.Remove(biggest.Min);
                }
            }
        }

        Console.Write("Biggest N values: [ ");
        foreach (var value in biggest)
        {
            Console.Write($"{value} ");
        }
        Console.WriteLine("]");
    }
}
