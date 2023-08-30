internal class Program
{


    private static void Main(string[] args)
    {
        Demo2();
    }


    private static void Demo2()
    {
        var Players = new List<Player> {
            new Player { Name = "Sue", Number = 1 },
            new Player { Name = "Fred", Number = 11 },
            new Player { Name = "Joe", Number = 2 },
            new Player { Name = "Sam", Number = 55 },
            new Player { Name = "Bob", Number = 3 },
            new Player { Name = "Sally", Number = 5 },
            new Player { Name = "Jane", Number = 10 },
            new Player { Name = "Sally", Number = 4},
            new Player { Name = "Tom", Number = 12 },
            new Player { Name = "Bill", Number = 13 },
        };

        var jerseys = new List<Jersey> {
            new Jersey { Number = 1, Size = 3 },
            new Jersey { Number = 2, Size = 2 },
            new Jersey { Number = 3, Size = 1 },
            new Jersey { Number = 55, Size = 6 },
            new Jersey { Number = 5, Size = 3 },
            new Jersey { Number = 10, Size = 2 },
            new Jersey { Number = 11, Size = 1 },
            new Jersey { Number = 12, Size = 3 },
            new Jersey { Number = 13, Size = 2 },
            new Jersey { Number = 4, Size = 6 },
        };

        var doubleDigitPlayers =
            from player in Players
            where player.Number > 10
            select player;

        var doubleDigitShirtSizes =
            from player in Players
            where player.Number > 10
            join shirt in jerseys
            on player.Number equals shirt.Number
            select shirt;

        var playersWithShirtSize =
            from player in Players
            where player.Number > 10
            join shirt in jerseys
            on player.Number equals shirt.Number
            orderby player.Number
            select new
            {
                player.Name,
                player.Number,
                shirt.Size
            };

        var allPlayersWithShirtSize =
            from player in Players
            join shirt in jerseys
            on player.Number equals shirt.Number
            orderby player.Number
            select new
            {
                player.Name,
                player.Number,
                shirt.Size
            };

        Console.WriteLine("Players with number greater than 10");
        foreach (var player in playersWithShirtSize )
        {
            Console.WriteLine($"Name: {player.Name} Number: {player.Number} Shirt Size: {player.Size}");
        }

        Console.WriteLine("All Players");
        foreach (var player in allPlayersWithShirtSize)
        {
            Console.WriteLine($"Name: {player.Name} Number: {player.Number} Shirt Size: {player.Size}");
        }

    }


    private static void Demo1()
    {
        var carList = new List<Car>();
        // Add 20 cars with at least 5 random colors
        var random = new Random();
        for (int i = 0; i < 20; i++)
        {
            carList.Add(new Car() {
                Color = random.Next(0, 5) switch {
                    0 => "Red",
                    1 => "Blue",
                    2 => "Green",
                    3 => "Yellow",
                    4 => "Black",
                    _ => "White"
                },
                Model = random.Next(0, 5) switch {
                    0 => "Ford",
                    1 => "Chevy",
                    2 => "Dodge",
                    3 => "Toyota",
                    4 => "Honda",
                    _ => "Nissan"
                },
                Year = random.Next(1990, 2021)
            });
        }

        var grouped =
            from car in carList
            group car by car.Color into colorGroup
            orderby colorGroup.Key
            select colorGroup;

        var grouped2 =
            carList.GroupBy(car => car.Color)
                .OrderBy(colorGroup => colorGroup.Key)
                .Select(colorGroup => colorGroup);

        Console.WriteLine("Cars:");
        foreach (var carGroup in grouped2)
        {
            Console.WriteLine(carGroup.Key);
            foreach (var car in carGroup)
            {
                Console.WriteLine(car);
            }
        }
    }
}

public class Car
{
    public string Color { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Year} {Color} {Model}";
    }
}

public class Player
{
    public string Name { get; set; } = default!;
    public int Number { get; set; }
}

public class Jersey
{
    public int Number { get; set; }
    public int Size { get; set; }
}