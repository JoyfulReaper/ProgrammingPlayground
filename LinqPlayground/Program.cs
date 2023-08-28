internal class Program
{


    private static void Main(string[] args)
    {
        Demo1();
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