using System.Collections.Immutable;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = ImmutableList.Create(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        var filteredList = FilterEvenNumbers(input);
        var filteredList2 = FilterEvenNumbers2(input);
    }

    private static ImmutableList<int> FilterEvenNumbers(ImmutableList<int> numbers)
    {
        return numbers.Where(n => n % 2 == 0).ToImmutableList();
    }

    private static ImmutableList<int> FilterEvenNumbers2(ImmutableList<int> numbers)
    {
        if (numbers.IsEmpty)
        {
            return ImmutableList<int>.Empty;
        }

        var head = numbers[0];
        var tail = numbers.RemoveAt(0);
        if (head % 2 == 0)
        {
            return ImmutableList<int>.Empty.Add(head).AddRange(FilterEvenNumbers2(tail));
        }
        else
        {
            return FilterEvenNumbers2(numbers.RemoveAt(0));
        }
    }
}