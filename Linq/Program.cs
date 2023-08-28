internal class Program
{
    private static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 99; i++)
        {
            numbers.Add(i);
        }
        IEnumerable<int> firstAndLastFive = numbers.Take(5).Concat(numbers.TakeLast(5));
        foreach (int i in firstAndLastFive)
        {
            Console.Write($"{i} ");
        }


        Console.WriteLine();
        int[] values = new int[] { 0, 12, 44, 36, 92, 54, 13, 8 };
        IEnumerable<int> result =
            from v in values
            where v < 37
            orderby -v
            select v;

        foreach (int i in result)
        {
            Console.Write($"{i} ");
        }

        // Modify every item returned from the query
        Console.WriteLine();
        var sandwiches = new[] {
            new { Name = "Salami With Mayo", Price = 4.95M },
            new { Name = "Ham and Cheese", Price = 6.95M },
            new { Name = "Chicken Cutlet", Price = 5.95M },
        };

        var sandwichesOnRye =
            from sandwich in sandwiches
            select $"{sandwich.Name} on Rye";

        foreach (string sandwich in sandwichesOnRye)
        {
            Console.WriteLine(sandwich);
        }

        // Preform calculations on sequences
        Console.WriteLine();
        var random = new Random();
        var numbersC = new List<int>();
        int length = random.Next(50, 150);
        for (int i = 0; i < length; i++)
        {
            numbersC.Add(random.Next(100));
        }

        Console.WriteLine($@"Stats for these {numbersC.Count} numbers:
        The first 5 numbers {String.Join(", ", numbersC.Take(5))}
        The last 5 numbers {String.Join(", ", numbersC.TakeLast(5))}
        The first is {numbersC.First()} and the last is {numbersC.Last()}
        The smallest is {numbersC.Min()} and the biggest is {numbersC.Max()}
        The sum is {numbersC.Sum()}
        The average is {numbersC.Average()}");

    }
}

