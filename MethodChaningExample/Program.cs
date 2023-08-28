internal class Program
{
    private static void Main(string[] args)
    {
        AddSubtract addSubtract = new AddSubtract() { Value = 5 }
            .Add(5)
            .Subtract(3)
            .Add(9)
            .Subtract(12);

        Console.WriteLine($"Final value: {addSubtract.Value}");
    }
}

public class AddSubtract
{
    public int Value { get; set; }

    public AddSubtract Add(int i)
    {
        Console.WriteLine($"Value: {Value}, adding {i}");
        return new AddSubtract { Value = Value + i };
    }

    public AddSubtract Subtract(int i)
    {
        Console.WriteLine($"Value: {Value}, subtracting {i}");
        return new AddSubtract { Value = Value - i };
    }
}
