using LinqExample;

internal class Program
{
    private static void Main(string[] args)
    {



        IEnumerable<Comic> mostExpensive =
            from comic in Comic.Catalog
            where Comic.Prices[comic.Issue] > 500
            orderby Comic.Prices[comic.Issue] descending
            select comic;

        foreach (Comic comic in mostExpensive)
        {
            Console.WriteLine($"{comic} is worth {Comic.Prices[comic.Issue]:c}");
        }


        var test =
            from comic in Comic.Catalog
            join review in Reviews
            on comic.Issue equals review.Issue
            select comic;
    }

    public static readonly IEnumerable<Review> Reviews = new[]
        {
            new Review() { Issue = 36, Critic = Critics.MuddyCritic, Score = 37.6 },
            new Review() { Issue = 74, Critic = Critics.RottenTornadoes, Score = 22.8 },
            new Review() { Issue = 74, Critic = Critics.MuddyCritic, Score = 84.2 },
            new Review() { Issue = 83, Critic = Critics.RottenTornadoes, Score = 89.4 },
            new Review() { Issue = 97, Critic = Critics.MuddyCritic, Score = 98.1 },
        };

}