// See https://aka.ms/new-console-template for more information

internal class Program
{
    public static void Main(string[] args)
    {
        const int grid_width = 5, grid_height = 4;
        const int segment_size = 5;

        for (int i = 0; i < grid_height * segment_size; i++)
        {
            for (int j = 0; j < grid_width * segment_size; j++)
            {
                if ( ((i + segment_size/2 + 1) % segment_size == 0)
                     || ((j + segment_size / 2 + 1) % segment_size == 0) )
                    Console.Write("#");
                else
                    Console.Write(" ");
                if ((j + 1) % segment_size == 0)
                    Console.Write(" ");
            }
            if ((i + 1) % segment_size == 0)
                Console.WriteLine();
            Console.WriteLine();
        }
        
    }
}